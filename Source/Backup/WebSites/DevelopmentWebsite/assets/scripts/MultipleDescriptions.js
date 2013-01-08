var uploadPane;

$(function () {
    uploadPane = $('#' + uploadPaneId);
    uploadPane.data('fileCount', 0);

    // In Safari java applets are not good in scroll.
    var ua = navigator.userAgent;
    if (/AppleWebKit/i.test(ua) && /Macintosh/i.test(ua)) {
        uploadPane.css('height', 'auto');
    }

});

function onUploadFileCountChange() {
    var f = this.files(),
				prevFileCount = uploadPane.data('fileCount'),
				fileCount = f.count(),
				guids = {},
				i;
    if (fileCount < prevFileCount) {

        // Files are being removed

        // Get upload file guids
        for (i = 0; i < fileCount; i++) {
            guids[f.get(i).guid() + ''] = true;
        }
        $(uploadPane).children('li').each(function (index, item) {
            var item = $(item), guid = item.data('guid');
            if (!guids[guid]) {
                item.remove();
            }
        });
    } else if (fileCount > prevFileCount) {

        // Files are being added

        for (var i = prevFileCount; i < fileCount; i++) {
            addItemToUploadPane(this, i);
        }
    }
    uploadPane.data('fileCount', fileCount);
}

function addItemToUploadPane(uploader, index) {
    var file = uploader.files().get(index),
				guid = file.guid();

    // Create new thumbnail control
    var thumbnail = $au.thumbnail({
        id: 'Thumbnail' + Math.round(Math.random() * 1000) + Math.round(Math.random() * 1000),
        width: '100px',
        height: '100px',
        parentControlName: uploader.id(),
        guid: guid,
        javaControl: {
            codeBase: uploader.javaControl().codeBase()
        },
        activeXControl: {
            codeBase: uploader.activeXControl().codeBase(),
            codeBase64: uploader.activeXControl().codeBase64()
        }
    });

    var data = {
        thumbnail: thumbnail.getHtml()
    };

    // Create list item html
    var html = $('#item-template').html().replace(/\$\{(\w+)\}/g, function (str, p) {
        return data[p];
    });

    // Append item to the list
    var item = $(html).appendTo(uploadPane).data('guid', guid);

    // Add remove button handler
    $('a.remove', item).click(function (ev) {

        // Remove item only from upload pane.
        // Removing file from upload pane triggers UploadFileCountChange, 
        // where we remove list item element.
        var guid = $(this).parent('li').data('guid');
        var files = uploader.files();
        for (var i = 0, imax = files.count(); i < imax; i++) {
            if (guid == files.get(i).guid() + '') {
                files.remove(i);
                break;
            }
        }

        ev.preventDefault();
    });
}

function onBeforeUpload() {
    var uploader = $au.uploader(uploaderId);
    $('li.upload-pane-item').each(function (index, item) {

        //Title will be sent as a custom Title_N POST field, where N is an 
        //index of the file.
        uploader.metadata().addCustomField('Title_' + index, $('input.title-field', item).val(), false);

        //Description will be sent as a native Description POST field 
        //provided by Image Uploader.
        uploader.files().get(index).description($('textarea.description-field', item).val());
    });
}