<?php
 /*
Plugin Name: MultiPowUpload
Plugin URI: http://www.element-it.com/wordpress-multiple-file-upload-plugin.aspx
Description: MultiPowUpload uploader is a fast and simple way to upload images and other files.  Automatically resize and upload pictures for your blog. 
Allow to generate thumbnails on client side and upload them to server. 
Rotate, crop operations, several resize modes, configurable thumbnails dimensions, watermarking.
List and thumbnails view.
Plugin will replace build-in WordPress flash uploader.
Version: 0.2.1
Author: Viktor Shelekhov, Element-IT software
  */
 
/*
  MultiPowUpload uploader plugin for Wordpress
  Copyright 2010  Viktor Shelekhov (email: support@element-it.com)

  This program is free software; you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation; either version 2 of the License, or
  (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License along
  with this program; if not, write to the Free Software Foundation, Inc.,
  51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
*/
 
 
//Configuration constants definition 
define('MPU_SERIAL_NUMBER_CFG',  'mpu_serial_number');
define('MPU_UI_DEFAUlT_VIEW_CFG',  'mpu_ui_default_view');
define('MPU_UI_LANGUAGE_AUTO_CFG',  'mpu_ui_language_auto');
define('MPU_UI_LANGUAGE_FILE_CFG',  'mpu_ui_language_file');
 
define('MPU_GENERATE_THUMBNAILS_CFG',  'mpu_generate_thumbnails');
define('MPU_THUMBNAILS_WIDTH_CFG',  'mpu_thumbnails_width');
define('MPU_THUMBNAILS_HEIGHT_CFG',  'mpu_thumbnails_height');
define('MPU_UPLOAD_ORIGINAL_IMAGE_CFG',  'mpu_upload_original_image');
define('MPU_THUMBNAIL_RESIZE_MODE_CFG',  'mpu_thumbnail_resize_mode');
define('MPU_THUMBNAIL_ALLOW_CROP_CFG',  'mpu_thumbnail_allow_crop');
define('MPU_THUMBNAIL_ALLOW_ROTATE_CFG',  'mpu_thumbnail_allow_rotate');

// thumbnail watermark configuration 
define('MPU_THUMBNAIL_WATERMARK_ENABLED_CFG', 'mpu_thumbnail_watermark_enabled');
define('MPU_THUMBNAIL_WATERMARK_TEXT_CFG', 'mpu_thumbnail_watermark_text');
define('MPU_THUMBNAIL_WATERMARK_URL_CFG', 'mpu_thumbnail_watermark_url');
define('MPU_THUMBNAIL_WATERMARK_ALPHA_CFG', 'mpu_thumbnail_watermark_alpha');
define('MPU_THUMBNAIL_WATERMARK_POSITION_CFG', 'mpu_thumbnail_watermark_position');
define('MPU_THUMBNAIL_WATERMARK_TEXT_FONT_CFG', 'mpu_thumbnail_watermark_text_font');
define('MPU_THUMBNAIL_WATERMARK_TEXT_COLOR_CFG', 'mpu_thumbnail_watermark_text_color');
define('MPU_THUMBNAIL_WATERMARK_TEXT_SIZE_CFG', 'mpu_thumbnail_watermark_text_size');
define('MPU_THUMBNAIL_WATERMARK_TEXT_STYLE_CFG', 'mpu_thumbnail_watermark_text_style');
 
$mpu_rel_dir = get_bloginfo('wpurl').'/'.'wp-content/plugins/multipowupload/';
$mpu_home_page = "http://www.element-it.com/wordpress-multiple-file-upload-plugin.aspx";
$mpu_swf_files = "http://www.element-it.com/multiple-file-upload/flash-uploader.aspx";

global $wp_version;
global $mpu_wpver;
// WP ME support, thanks to Bono-san http://bono.s201.xrea.com/
$mpu_wpver = str_replace("ME", "", $wp_version);
$mpu_wpver = explode('.', $mpu_wpver);

/**
 * set default values for plugin options
 * should be called to load default values
 */
function mpu_configure() 
{
    // Set default options
	if (get_option(MPU_SERIAL_NUMBER_CFG) == false) 	
        add_option(MPU_SERIAL_NUMBER_CFG, '');
		
	if (get_option(MPU_UI_DEFAUlT_VIEW_CFG) == false) 	
        add_option(MPU_UI_DEFAUlT_VIEW_CFG, 'thumbnails');
		
	if (get_option(MPU_UI_LANGUAGE_AUTO_CFG) == false) 	
        add_option(MPU_UI_LANGUAGE_AUTO_CFG, 'true');
	
	if (get_option(MPU_UI_LANGUAGE_FILE_CFG) == false) 	
        add_option(MPU_UI_LANGUAGE_FILE_CFG, 'mpu_languages/Language_<LANGUAGE_CODE>.xml');
	
	//thumbnails configuration
    if (get_option(MPU_GENERATE_THUMBNAILS_CFG) == false) 	
        add_option(MPU_GENERATE_THUMBNAILS_CFG, 'true');
       
	if (get_option(MPU_THUMBNAILS_WIDTH_CFG) == false) 	
        add_option(MPU_THUMBNAILS_WIDTH_CFG, '640');
       
	if (get_option(MPU_THUMBNAILS_HEIGHT_CFG) == false) 
        add_option(MPU_THUMBNAILS_HEIGHT_CFG, '480');
    	
	if (get_option(MPU_UPLOAD_ORIGINAL_IMAGE_CFG) == false || get_option(MPU_GENERATE_THUMBNAILS_CFG) == 'false')
        add_option(MPU_UPLOAD_ORIGINAL_IMAGE_CFG, 'true');
    
	if (get_option(MPU_THUMBNAIL_RESIZE_MODE_CFG) == false)
        add_option(MPU_THUMBNAIL_RESIZE_MODE_CFG, 'fit');
	
	if (get_option(MPU_THUMBNAIL_ALLOW_CROP_CFG) == false) 	
        add_option(MPU_THUMBNAIL_ALLOW_CROP_CFG, 'true');
		
	if (get_option(MPU_THUMBNAIL_ALLOW_ROTATE_CFG) == false) 	
        add_option(MPU_THUMBNAIL_ALLOW_ROTATE_CFG, 'true');
	
	// thumbnail watermark configuration 
	if (get_option(MPU_THUMBNAIL_WATERMARK_ENABLED_CFG) == false) 	
        add_option(MPU_THUMBNAIL_WATERMARK_ENABLED_CFG, 'true');
	
	if (get_option(MPU_THUMBNAIL_WATERMARK_TEXT_CFG) == false) 	
        add_option(MPU_THUMBNAIL_WATERMARK_TEXT_CFG, get_bloginfo('name'));
	
	if (get_option(MPU_THUMBNAIL_WATERMARK_URL_CFG) == false) 	
        add_option(MPU_THUMBNAIL_WATERMARK_URL_CFG, '');
		
	if (get_option(MPU_THUMBNAIL_WATERMARK_ALPHA_CFG) == false) 	
        add_option(MPU_THUMBNAIL_WATERMARK_ALPHA_CFG, '0.6');
	
	if (get_option(MPU_THUMBNAIL_WATERMARK_POSITION_CFG) == false) 	
        add_option(MPU_THUMBNAIL_WATERMARK_POSITION_CFG, 'center.center');
	
	if (get_option(MPU_THUMBNAIL_WATERMARK_TEXT_FONT_CFG) == false) 	
        add_option(MPU_THUMBNAIL_WATERMARK_TEXT_FONT_CFG, '_sans');
	
	if (get_option(MPU_THUMBNAIL_WATERMARK_TEXT_COLOR_CFG) == false) 	
        add_option(MPU_THUMBNAIL_WATERMARK_TEXT_COLOR_CFG, '#FF0000');
	
	if (get_option(MPU_THUMBNAIL_WATERMARK_TEXT_SIZE_CFG) == false) 	
        add_option(MPU_THUMBNAIL_WATERMARK_TEXT_SIZE_CFG, '22');
		
	if (get_option(MPU_THUMBNAIL_WATERMARK_TEXT_STYLE_CFG) == false) 	
        add_option(MPU_THUMBNAIL_WATERMARK_TEXT_STYLE_CFG, 'normal');
	
}

mpu_configure();

/*
Replace build-in WP flash uploader with MultiPowUpload
only if we have required swf files in plugin subfolder
*/
if(@file_exists(dirname(__FILE__)."/ElementITMultiPowUpload.swf"))
{
	add_action('pre-upload-ui', 'multipowupload_add_js_code');
	add_action('pre-flash-upload-ui', 'multipowupload_open_comment');
	add_action('post-flash-upload-ui', 'multipowupload_close_comment');
	/*add_action('pre-plupload-upload-ui', 'multipowupload_open_comment');*/
	add_action('post-plupload-upload-ui', 'multipowupload_write_plswitch');
	add_action('post-html-upload-ui', 'multipowupload_write_htmlswitch');
}


/**
 * Called only if flash uploader enabled. 
**/
function multipowupload_open_comment()
{
	global $mpu_wpver; 
	//Firstly we should close comment tag wich was opened for SWFUpload script tag
	echo "-->";
	if (($mpu_wpver[0] == 2) && ($mpu_wpver[1] < 8)) 
	{
		echo "\n
		//]]>";
	}
	
	//add own <div> tags wich will be replaced with needed html tags by SWFObject
	multipowupload_add_interface();
	//Start comment tag for build-in html tags wich contain browse and cancel buttons for SWFUpload
	if (($mpu_wpver[0] == 3 && $mpu_wpver[1] < 3) || $mpu_wpver[0] < 3) 
		echo "<!--";
}

/**
 * Called only if flash uploader enabled. 
**/
function multipowupload_close_comment()
{
	//finally close comment tag for build-in SWFUpload
	if (($mpu_wpver[0] == 3 && $mpu_wpver[1] < 3) || $mpu_wpver[0] < 3) 
		echo "-->";
}

/**
 * Show link to switch to th MPU interface
**/
function multipowupload_write_plswitch()
{
	echo '<p class="upload-flash-bypass">'.
		 _e('You are using build-in uploader. <a id="pl-switch-to-mpu" href="#">Switch to the MultiPowUpload uploader</a>.').
		'</p>';
}

/**
 * Show link to switch to th MPU interface
**/
function multipowupload_write_htmlswitch()
{
	global $mpu_wpver; 
	if ((($mpu_wpver[0] >= 3) && ($mpu_wpver[1] >= 3)))
		echo '<p class="upload-flash-bypass">'.
		 _e('You are using build-in uploader. <a id="html-switch-to-mpu" href="#">Switch to the MultiPowUpload uploader</a>.').
		'</p>';
}


/**
 * Method add MultiPowUPload JS code to page if flash uploader enabled
 * and open comment tag for	WP flash uploader js code
**/
function multipowupload_add_js_code()
{
	global $type, $tab, $mpu_rel_dir;
	global $mpu_wpver; 
	
	/* code from media.php WP script*/
	$flash_action_url = admin_url('async-upload.php');
	
	// If Mac and mod_security, no Flash. :(
	$flash = true;
	if ( false !== stripos($_SERVER['HTTP_USER_AGENT'], 'mac') && apache_mod_loaded('mod_security') )
		$flash = false;

	$flash = apply_filters('flash_uploader', $flash);
	
	$post_id = isset($_REQUEST['post_id']) ? intval($_REQUEST['post_id']) : 0;
	$upload_size_unit = $max_upload_size =  wp_max_upload_size();
	$sizes = array( 'KB', 'MB', 'GB' );
	for ( $u = -1; $upload_size_unit > 1024 && $u < count( $sizes ) - 1; $u++ )
		$upload_size_unit /= 1024;
	if ( $u < 0 ) {
		$upload_size_unit = 0;
		$u = 0;
	} else {
		$upload_size_unit = (int) $upload_size_unit;
	}

if($flash): ?>
<script type="text/javascript" src="<? echo $mpu_rel_dir; ?>mpu.js">
</script>

<!--SwfObject  was removed from wp 3.3 distribution pckage, so we should include it manually-->
<?php 
//print_r($mpu_wpver);
if (($mpu_wpver[0] == 3) && ($mpu_wpver[1] > 2))
	echo '<script type="text/javascript" src="'. $mpu_rel_dir.'swfobject.js"></script>';
?>
<script type="text/javascript">
	//init swfu object with custom settings, because we have commented it out
	var swfu = {"customSettings":{"swfupload_element_id":"flash-upload-ui", "degraded_element_id":"html-upload-ui"}}
	//emulate function that return count of files in queue
	swfu.getStats = function()
	{		
		return countFilesInQueue();
	};
	
	//call swfuploadPreLoad methjod on document initialization to show needed upload interface
	<?php if (!(($mpu_wpver[0] == 3) && ($mpu_wpver[1] > 2)))
	echo '
		jQuery(document).ready(function($)
		{	
			swfuploadPreLoad();
		});';
	else
		echo "jQuery(document).ready(function($)		{	
				$('#plupload-upload-ui').css('display', 'none');
				$('#html-upload-ui').css('display', 'none');
				$('#mpu-switch-to-buildin').click(function()	{
					$('#flash-upload-ui').css('display', 'none');
					$('#plupload-upload-ui').css('display', '');
					$('#html-upload-ui').css('display', '');
					$('#media-items, p.submit, span.big-file-warning').css('display', '');
					switchUploader(1);
				});
				$('#pl-switch-to-mpu, #html-switch-to-mpu').click(function()	{
					$('#flash-upload-ui').css('display', '');
					$('#plupload-upload-ui').css('display', 'none');
					$('#html-upload-ui').css('display', 'none');
					$('#media-items, p.submit, span.big-file-warning').css('display', '');					
				});
		});";
	
		
	?>
	
	//MultiPowUpload configuration code
	var params = {  
		BGColor: "#FFFFFF"
	};
	
	var attributes = {  
		id: "MultiPowUpload",  
		name: "MultiPowUpload"
	};
	
	var flashvars = {
	  "serialNumber":"<?php echo get_option(MPU_SERIAL_NUMBER_CFG); ?>",
	  "uploadUrl": "<?php echo $flash_action_url ; ?>",
	  "useExternalInterface": "true",
	  "sendThumbnails": "<?php echo get_option(MPU_GENERATE_THUMBNAILS_CFG);?>",
	  "postFields.file": "async-upload",
	  "postFields.thumbnail": "async-upload",
	  "fileFilter.types": "Allowed files| <?php echo apply_filters('upload_file_glob', '*.*'); ?>",	  
	  "fileFilter.maxSize" : "<?php echo $max_upload_size; ?>",
	  "sendOriginalImages": "<?php echo get_option(MPU_UPLOAD_ORIGINAL_IMAGE_CFG);?>",
	  "fileView.defaultView": "<?php echo get_option(MPU_UI_DEFAUlT_VIEW_CFG);?>",	  
	  "thumbnail.width": "<?php echo get_option(MPU_THUMBNAILS_WIDTH_CFG);?>",
	  "thumbnail.height": "<?php echo get_option(MPU_THUMBNAILS_HEIGHT_CFG);?>",
	  "thumbnail.resizeMode": "<?php echo get_option(MPU_THUMBNAIL_RESIZE_MODE_CFG);?>",
	  "thumbnailView.allowRotate": "<?php echo get_option(MPU_THUMBNAIL_ALLOW_ROTATE_CFG);?>",
	  "thumbnailView.allowCrop": "<?php echo get_option(MPU_THUMBNAIL_ALLOW_CROP_CFG);?>",
	  "thumbnail.watermark.enabled": "<?php echo get_option(MPU_THUMBNAIL_WATERMARK_ENABLED_CFG);?>",
	  "thumbnail.watermark.alpha": "<?php echo get_option(MPU_THUMBNAIL_WATERMARK_ALPHA_CFG);?>",
	  "thumbnail.watermark.imageUrl": "<?php echo get_option(MPU_THUMBNAIL_WATERMARK_URL_CFG);?>",
	  "thumbnail.watermark.position": "<?php echo get_option(MPU_THUMBNAIL_WATERMARK_POSITION_CFG);?>",
	  "thumbnail.watermark.text": "<?php echo get_option(MPU_THUMBNAIL_WATERMARK_TEXT_CFG);?>",
	  "thumbnail.watermark.textStyle.color": "<?php echo get_option(MPU_THUMBNAIL_WATERMARK_TEXT_COLOR_CFG);?>",
	  "thumbnail.watermark.textStyle.font": "<?php echo get_option(MPU_THUMBNAIL_WATERMARK_TEXT_FONT_CFG);?>",
	  "thumbnail.watermark.textStyle.size": "<?php echo get_option(MPU_THUMBNAIL_WATERMARK_TEXT_SIZE_CFG);?>",
	  "thumbnail.watermark.textStyle.style": "<?php echo get_option(MPU_THUMBNAIL_WATERMARK_TEXT_STYLE_CFG);?>",	  
	  "language.autoDetect": "<?php echo get_option(MPU_UI_LANGUAGE_AUTO_CFG);?>",
	  "language.source": "<?php echo $mpu_rel_dir.get_option(MPU_UI_LANGUAGE_FILE_CFG);?>"	  

	};
	
//Default MultiPowUpload should have minimum width=410 and minimum height=180
	swfobject.embedSWF("<?php echo $mpu_rel_dir; ?>ElementITMultiPowUpload.swf", "MultiPowUpload_holder", "420", "250", "10.0.0", "expressInstall.swf", flashvars, params, attributes);
	
	function MultiPowUpload_onMovieLoad()
	{		
		
		
		
		MultiPowUpload.addPostField("post_id", "<?php echo $post_id; ?>");
		MultiPowUpload.addPostField("auth_cookie", "<?php echo (is_ssl() ? $_COOKIE[SECURE_AUTH_COOKIE] : $_COOKIE[AUTH_COOKIE]); ?>");
		MultiPowUpload.addPostField("logged_in_cookie", "<?php echo $_COOKIE[LOGGED_IN_COOKIE]; ?>");
		MultiPowUpload.addPostField("_wpnonce", "<?php echo wp_create_nonce('media-form'); ?>");
		MultiPowUpload.addPostField("type", "<?php echo $type; ?>");
		MultiPowUpload.addPostField("tab", "<?php echo $tab; ?>");
		MultiPowUpload.addPostField("short", "1");	

		
	}
</script>
<?php	
	if (($mpu_wpver[0] == 2) && ($mpu_wpver[1] < 8)) 
			echo "<![CDATA[";
	

if(($mpu_wpver[0] == 3 && $mpu_wpver[1] < 3) || $mpu_wpver[0] < 3) 
	echo '<!-- comment for <div> tag  with SWFUpload flash movie  and replace it with out own tag'. (($mpu_wpver[0] == 3) && ($mpu_wpver[2] < 3)) .'-'. ($mpu_wpver[0] < 3);
else
{
	multipowupload_add_interface();
	echo "</div>";
}

endif; 
}

/**
 * Method add div tag - placeholder for Flash movie
 * 
**/
function multipowupload_add_interface( $errors = null ) 
{
	global $mpu_rel_dir, $mpu_wpver;
	$upload_size_unit = $max_upload_size =  wp_max_upload_size();
	$sizes = array( 'KB', 'MB', 'GB' );
	for ( $u = -1; $upload_size_unit > 1024 && $u < count( $sizes ) - 1; $u++ )
		$upload_size_unit /= 1024;
	if ( $u < 0 ) {
		$upload_size_unit = 0;
		$u = 0;
	} else {
		$upload_size_unit = (int) $upload_size_unit;
	}
?>
	<div id="flash-upload-ui">
	<div id="MultiPowUpload_holder">
	<strong>You need at least 10 version of Flash player!</strong>
	<a href="http://www.adobe.com/go/getflashplayer">&nbsp;<img border="0" src="<?php echo $mpu_rel_dir;?>get_flash_player.gif" alt="Get Adobe Flash player" /></a>
	</div>
		<?php if(($mpu_wpver[0] == 3 && $mpu_wpver[1] < 3) || $mpu_wpver[0] < 3) { ?>
			<p class="media-upload-size"><?php printf( __( 'Maximum upload file size: %d%s' ), $upload_size_unit, $sizes[$u] ); ?></p>
		<?php } else{ ?>
		<p class="upload-flash-bypass">
		<?php _e('You are using MultiPowUpload uploader. <a id="mpu-switch-to-buildin" href="#">Switch to the build-in uploader</a>.'); ?>
		</p>
		<?php } ?>
		
		

<?php
}


// Multi-language support
if (defined('WPLANG') && function_exists('load_plugin_textdomain')) {
    load_plugin_textdomain('mpu', $fup_rel_dir.'languages');
}

/**
 * add_mpu_config_page
 * 
 * Adds MultiPowUpload upload configuration page to the admin tab
 */
function add_mpu_config_page() {
    add_options_page('MultiPowUpload config', 'MultiPowUpload', 8,
                     basename(__FILE__), 'mpu_config_page');
}

add_action('admin_menu', 'add_mpu_config_page');

/**
 * mpu_config_page
 * 
 * MultiPowUpload configuration page.
 */
function mpu_config_page() 
{
	global $mpu_rel_dir, $mpu_swf_files, $mpu_home_page;
	
    if ('update' == $_POST['action']) 
	{
        if ( function_exists('current_user_can') && !current_user_can('manage_options') ) 		
            die(__('You do not have sufficient permission to manage options.', 'mpu'));        

        $updare_error_message = '';
		
		if(isset($_POST[MPU_SERIAL_NUMBER_CFG]) )
			update_option(MPU_SERIAL_NUMBER_CFG, $_POST[MPU_SERIAL_NUMBER_CFG]);
		
		switch ($_POST[MPU_UI_DEFAUlT_VIEW_CFG]) 
		{
            case 'list':
            case 'thumbnails':            
                update_option(MPU_UI_DEFAUlT_VIEW_CFG, $_POST[MPU_UI_DEFAUlT_VIEW_CFG]);
                break;

            default:
                $updare_error_message .= ($updare_error_message!=''?'<br />':'');
                $updare_error_message .= sprintf(__('Wrong value %s for %s', 'mpu'), $_POST[MPU_UI_DEFAUlT_VIEW_CFG], MPU_UI_DEFAUlT_VIEW_CFG);
        }
		
		if (isset($_POST[MPU_UI_LANGUAGE_AUTO_CFG]) && $_POST[MPU_UI_LANGUAGE_AUTO_CFG] == 'true') 
		{
            update_option(MPU_UI_LANGUAGE_AUTO_CFG, 'true');
			update_option(MPU_UI_LANGUAGE_FILE_CFG, 'mpu_languages/Language_<LANGUAGE_CODE>.xml');
		}
        else 
		{
            update_option(MPU_UI_LANGUAGE_AUTO_CFG, 'false');     
			if (isset($_POST[MPU_UI_LANGUAGE_FILE_CFG])) 
				update_option(MPU_UI_LANGUAGE_FILE_CFG, $_POST[MPU_UI_LANGUAGE_FILE_CFG]);
			else
				delete_option(MPU_UI_LANGUAGE_FILE_CFG);
			
		}	
	 
	
		if (isset($_POST[MPU_GENERATE_THUMBNAILS_CFG]) && $_POST[MPU_GENERATE_THUMBNAILS_CFG] == 'true') 
            update_option(MPU_GENERATE_THUMBNAILS_CFG, 'true');        
        else 
            update_option(MPU_GENERATE_THUMBNAILS_CFG, 'false');        
		
        if (($_POST[MPU_THUMBNAILS_WIDTH_CFG] >= 0) && ($_POST[MPU_THUMBNAILS_WIDTH_CFG] <= 4096))
            update_option(MPU_THUMBNAILS_WIDTH_CFG, $_POST[MPU_THUMBNAILS_WIDTH_CFG]);        
        else 
		{
            $updare_error_message .= ($updare_error_message!='' ? '<br />' : '');
            $updare_error_message .= sprintf(__('Wrong value %d for %s', 'mpu'), $_POST[MPU_THUMBNAILS_WIDTH_CFG], MPU_THUMBNAILS_WIDTH_CFG);
        }
		
		if(($_POST[MPU_THUMBNAILS_HEIGHT_CFG] >= 0) && ($_POST[MPU_THUMBNAILS_HEIGHT_CFG] <= 4096))
            update_option(MPU_THUMBNAILS_HEIGHT_CFG, $_POST[MPU_THUMBNAILS_HEIGHT_CFG]);        
        else 
		{
            $updare_error_message .= ($updare_error_message!='' ? '<br />' : '');
            $updare_error_message .= sprintf(__('Wrong value %d for %s', 'mpu'), $_POST[MPU_THUMBNAILS_HEIGHT_CFG], MPU_THUMBNAILS_HEIGHT_CFG);
        }
      
		if(isset($_POST[MPU_UPLOAD_ORIGINAL_IMAGE_CFG]) && $_POST[MPU_UPLOAD_ORIGINAL_IMAGE_CFG] == 'true')
            update_option(MPU_UPLOAD_ORIGINAL_IMAGE_CFG, 'true');        
        else
            update_option(MPU_UPLOAD_ORIGINAL_IMAGE_CFG, 'false');
			
			
        switch ($_POST[MPU_THUMBNAIL_RESIZE_MODE_CFG]) 
		{
            case 'fit':
			case 'fitByWidth':
			case 'fitByHeight':
			case 'exactFit':
            case 'stretch':
            case 'crop':            
                update_option(MPU_THUMBNAIL_RESIZE_MODE_CFG, $_POST[MPU_THUMBNAIL_RESIZE_MODE_CFG]);
                break;

            default:
                $updare_error_message .= ($updare_error_message!=''?'<br />':'');
                $updare_error_message .= sprintf(__('Wrong value %s for %s', 'mpu'), $_POST[MPU_THUMBNAIL_RESIZE_MODE_CFG], MPU_THUMBNAIL_RESIZE_MODE_CFG);
        }
		
		if(isset($_POST[MPU_THUMBNAIL_ALLOW_CROP_CFG]) && $_POST[MPU_THUMBNAIL_ALLOW_CROP_CFG] == 'true') 
            update_option(MPU_THUMBNAIL_ALLOW_CROP_CFG, 'true');        
        else 
            update_option(MPU_THUMBNAIL_ALLOW_CROP_CFG, 'false');        
		
		if(isset($_POST[MPU_THUMBNAIL_ALLOW_ROTATE_CFG]) && $_POST[MPU_THUMBNAIL_ALLOW_ROTATE_CFG] == 'true')
            update_option(MPU_THUMBNAIL_ALLOW_ROTATE_CFG, 'true');        
        else 
            update_option(MPU_THUMBNAIL_ALLOW_ROTATE_CFG, 'false');
	
		// thumbnail watermark configuration 
		if(isset($_POST[MPU_THUMBNAIL_WATERMARK_ENABLED_CFG]) && $_POST[MPU_THUMBNAIL_WATERMARK_ENABLED_CFG] == 'true')
            update_option(MPU_THUMBNAIL_WATERMARK_ENABLED_CFG, 'true');        
        else 
            update_option(MPU_THUMBNAIL_WATERMARK_ENABLED_CFG, 'false');
	
		if(isset($_POST[MPU_THUMBNAIL_WATERMARK_TEXT_CFG]))
            update_option(MPU_THUMBNAIL_WATERMARK_TEXT_CFG, $_POST[MPU_THUMBNAIL_WATERMARK_TEXT_CFG]);        
        else 
            delete_option(MPU_THUMBNAIL_WATERMARK_TEXT_CFG, null);
		
		if(isset($_POST[MPU_THUMBNAIL_WATERMARK_URL_CFG]))
            update_option(MPU_THUMBNAIL_WATERMARK_URL_CFG, $_POST[MPU_THUMBNAIL_WATERMARK_URL_CFG]);        
        else 
            delete_option(MPU_THUMBNAIL_WATERMARK_URL_CFG, null);
		
		if(($_POST[MPU_THUMBNAIL_WATERMARK_ALPHA_CFG] >= 0) && ($_POST[MPU_THUMBNAIL_WATERMARK_ALPHA_CFG] <= 1))
            update_option(MPU_THUMBNAIL_WATERMARK_ALPHA_CFG, $_POST[MPU_THUMBNAIL_WATERMARK_ALPHA_CFG]);        
        else 
		{
            $updare_error_message .= ($updare_error_message!='' ? '<br />' : '');
            $updare_error_message .= sprintf(__('Wrong value %d for %s', 'mpu'), $_POST[MPU_THUMBNAIL_WATERMARK_ALPHA_CFG], MPU_THUMBNAIL_WATERMARK_ALPHA_CFG);
        }
		
		if(strrpos ($_POST[MPU_THUMBNAIL_WATERMARK_POSITION_CFG],".") !== FALSE)
		{
			$pos = explode(".", $_POST[MPU_THUMBNAIL_WATERMARK_POSITION_CFG]);
			$values = array('left', 'right', 'center', 'top', 'bottom');
			if(count($pos) == 2 && in_array($pos[0],$values) && in_array($pos[1],$values))
				update_option(MPU_THUMBNAIL_WATERMARK_POSITION_CFG, $_POST[MPU_THUMBNAIL_WATERMARK_POSITION_CFG]);        
			 else 
			{
				$updare_error_message .= ($updare_error_message!='' ? '<br />' : '');
				$updare_error_message .= sprintf(__('Wrong value %d for %s', 'mpu'), $_POST[MPU_THUMBNAIL_WATERMARK_POSITION_CFG], MPU_THUMBNAIL_WATERMARK_POSITION_CFG);
			}
		}
        else 
		{
            $updare_error_message .= ($updare_error_message!='' ? '<br />' : '');
            $updare_error_message .= sprintf(__('Wrong value %d for %s', 'mpu'), $_POST[MPU_THUMBNAIL_WATERMARK_POSITION_CFG], MPU_THUMBNAIL_WATERMARK_POSITION_CFG);
        }
				
		if(isset($_POST[MPU_THUMBNAIL_WATERMARK_TEXT_FONT_CFG]))
            update_option(MPU_THUMBNAIL_WATERMARK_TEXT_FONT_CFG, $_POST[MPU_THUMBNAIL_WATERMARK_TEXT_FONT_CFG]);        
        else 
            delete_option(MPU_THUMBNAIL_WATERMARK_TEXT_FONT_CFG, null);
			
		if(isset($_POST[MPU_THUMBNAIL_WATERMARK_TEXT_COLOR_CFG]))
            update_option(MPU_THUMBNAIL_WATERMARK_TEXT_COLOR_CFG, $_POST[MPU_THUMBNAIL_WATERMARK_TEXT_COLOR_CFG]);        
        else 
            delete_option(MPU_THUMBNAIL_WATERMARK_TEXT_COLOR_CFG, null);
		
		if(isset($_POST[MPU_THUMBNAIL_WATERMARK_TEXT_SIZE_CFG]))
            update_option(MPU_THUMBNAIL_WATERMARK_TEXT_SIZE_CFG, $_POST[MPU_THUMBNAIL_WATERMARK_TEXT_SIZE_CFG]);        
        else 
            delete_option(MPU_THUMBNAIL_WATERMARK_TEXT_SIZE_CFG, null);
		
		if(isset($_POST[MPU_THUMBNAIL_WATERMARK_TEXT_STYLE_CFG]))
            update_option(MPU_THUMBNAIL_WATERMARK_TEXT_STYLE_CFG, $_POST[MPU_THUMBNAIL_WATERMARK_TEXT_STYLE_CFG]);        
        else 
            delete_option(MPU_THUMBNAIL_WATERMARK_TEXT_STYLE_CFG, null);
		
        
        if ($updare_error_message) 		
            echo '<div class="error"><p>'.__('Error encountered while updating options: ', 'mpu').$updare_error_message.'</p></div>';        
        else 		
            echo '<div class="updated"><p>'.__('Options updated.', 'mpu').'</p></div>';
    }
	
	
?>
<form method="post" action="<?php echo $_SERVER['REQUEST_URI']; ?>">
<div class="wrap">
  
  <h2><?php _e('WP System Configuration', 'mpu'); ?></h2>

  <ul>
  <li><?php printf(__('System memory limit: <b>%s</b>', 'mpu'), ini_get('memory_limit')); ?></li>
  <li><?php printf(__('Maximum uploadable file size: <b>%s</b>', 'mpu'), ini_get('upload_max_filesize')); ?><br />
  <?php printf(__('Make sure this size is large enough, you won\'t be able to upload an file whose size is bigger than this value. (see %sPHP help about upload_max_filesize%s)', 'mpu'), '<a href="http://www.php.net/manual/en/ini.core.php#ini.upload-max-filesize">', '</a>'); ?></li>
  
  </ul>

  <h2><?php _e('MultiPowUpload configuration', 'mpu'); ?></h2>

  <?php
  if(!@file_exists(dirname(__FILE__)."/ElementITMultiPowUpload.swf"))
			 echo '<div class="error"><p>'.sprintf(__('Required <b>ElementITMultiPowUpload.swf</b> file is missed. May be it is your first launch of MultiPowUpload uploader plugin configuration. <br/> We not include these files into plugin distribution package because of incompatible licenses. Plugin licensed under GPL license while swf file licensed under commercial license. <br/> If you are still interested, please download missed swf files from %shere%s . <br/> Once you have done with downloading, unzip archive and place swf files at <b>wp-content/plugins/multipowupload/</b> subfolder of your WordPress web site.', 'mpu'), '<a href="'.$mpu_swf_files.'">', '</a>').'</p></div>';    
  ?>
  <fieldset class='options'>    

    <table class="editform" cellspacing="2" cellpadding="5" width="100%">				
		<tr>
        <th width="30%" valign="top" style="padding-top: 10px;">
          <?php _e('General options', 'mpu'); ?>
        </th>
        <td>          
        </td>
      </tr>
		<tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_SERIAL_NUMBER_CFG ?>"><?php _e('Serial number', 'mpu'); ?></label>
        </td>
        <td>
          <?php
          echo "<input type='text' size='50' ";
          echo "name='".MPU_SERIAL_NUMBER_CFG."' ";
          echo "id='".MPU_SERIAL_NUMBER_CFG."' ";
          echo "value='".get_option(MPU_SERIAL_NUMBER_CFG)."' />";
          echo "\n";
          ?><br />
          <?php printf(__('Place your serial number here to register MultiPowUpload. Warning window about trial version will be removed in full version. Visit %s Element-IT web site%s for more information.', 'mpu'), '<a href="'.$mpu_home_page.'">', '</a>'); ?>
        </td>
      </tr>	
		
		<tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_UI_DEFAUlT_VIEW_CFG; ?>"><?php
                     _e('Default file view ', 'mpu') ?></label>
        </td>
        <td>
          <?php
          echo "<select name='".MPU_UI_DEFAUlT_VIEW_CFG
               ."' id='".MPU_UI_DEFAUlT_VIEW_CFG."'>\n"
               ."<option value='list'";
          if(get_option(MPU_UI_DEFAUlT_VIEW_CFG) == 'list')
              echo " selected='selected'";
          echo ">".__('list', 'mpu')."</option>\n"
               ."<option value='thumbnails'";
          if(get_option(MPU_UI_DEFAUlT_VIEW_CFG) == 'thumbnails')
              echo" selected='selected'";
          echo ">".__('thumbnails', 'mpu')."</option>\n"
               ."</select>";
          ?><br />
          <?php
              _e('Default view of files. List or thumbnails.', 'mpu');
          ?>
        </td>
      </tr>
	  <tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_UI_LANGUAGE_AUTO_CFG ?>"><?php
                     _e('Autodetect user language', 'mpu') ?></label>
        </td>
        <td>
          <?php
          echo "<input type='checkbox' name='".MPU_UI_LANGUAGE_AUTO_CFG
               ."' id='".MPU_UI_LANGUAGE_AUTO_CFG."' value='true'";
          if (get_option(MPU_UI_LANGUAGE_AUTO_CFG) == 'true') {
              echo " checked='checked'";
          }
          echo " />\n";
          ?><br />
          <?php
              _e('Check this box if you want allow MultiPowUpload automatically detect user language and load appropriate translation file..', 'mpu');
          ?>
        </td>
      </tr>
	   <tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_UI_LANGUAGE_FILE_CFG ?>"><?php _e('Xml translation file', 'mpu'); ?></label>
        </td>
        <td>
          <?php
          echo "<input type='text' size='50' ";
          echo "name='".MPU_UI_LANGUAGE_FILE_CFG."' ";
          echo "id='".MPU_UI_LANGUAGE_FILE_CFG."' ";
          echo "value='".get_option(MPU_UI_LANGUAGE_FILE_CFG)."' />";
          echo "\n";
          ?><br />
          <?php _e('Url to xml  translation file. If value does not contain <LANGUAGE_CODE> placeholder, language detection will not be performed.  ', 'mpu'); ?>
        </td>
      </tr>

		<tr>
        <th width="30%" valign="top" style="padding-top: 10px;">
          <?php _e('Thumbnails upload options', 'mpu'); ?>
        </th>
        <td>          
        </td>
      </tr>
		<tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_GENERATE_THUMBNAILS_CFG ?>"><?php
                     _e('Generate and upload thumbnails', 'mpu') ?></label>
        </td>
        <td>
          <?php
          echo "<input type='checkbox' name='".MPU_GENERATE_THUMBNAILS_CFG
               ."' id='".MPU_GENERATE_THUMBNAILS_CFG."' value='true'";
          if (get_option(MPU_GENERATE_THUMBNAILS_CFG) == 'true') {
              echo " checked='checked'";
          }
          echo " />\n";
          ?><br />
          <?php
              _e('Check this box if you want generate and upload image thumbnails.', 'mpu');
          ?>
        </td>
      </tr>
	  

      <tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_THUMBNAILS_WIDTH_CFG ?>"><?php _e('Thumbnail width', 'mpu'); ?></label>
        </td>
        <td>
          <?php
          echo "<input type='text' size='8' ";
          echo "name='".MPU_THUMBNAILS_WIDTH_CFG."' ";
          echo "id='".MPU_THUMBNAILS_WIDTH_CFG."' ";
          echo "value='".get_option(MPU_THUMBNAILS_WIDTH_CFG)."' />";
          _e(' (px)', 'mpu'); echo "\n";
          ?><br />
          <?php _e('Specify the default width to which you want to resize your pictures. ', 'mpu'); ?>
        </td>
      </tr>
	  <tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_THUMBNAILS_HEIGHT_CFG ?>"><?php _e('Thumbnail height', 'mpu'); ?></label>
        </td>
        <td>
          <?php
          echo "<input type='text' size='8' ";
          echo "name='".MPU_THUMBNAILS_HEIGHT_CFG."' ";
          echo "id='".MPU_THUMBNAILS_HEIGHT_CFG."' ";
          echo "value='".get_option(MPU_THUMBNAILS_HEIGHT_CFG)."' />";
          _e(' (px)', 'mpu'); echo "\n";
          ?><br />
          <?php _e('Specify the default height to which you want to resize your pictures. ', 'mpu'); ?>
        </td>
      </tr>
	  <tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_UPLOAD_ORIGINAL_IMAGE_CFG ?>"><?php
                     _e('Upload original image?', 'mpu') ?></label>
        </td>
        <td>
          <?php
          echo "<input type='checkbox' name='".MPU_UPLOAD_ORIGINAL_IMAGE_CFG
               ."' id='".MPU_UPLOAD_ORIGINAL_IMAGE_CFG."' value='true'";
          if (get_option(MPU_UPLOAD_ORIGINAL_IMAGE_CFG) == 'true') {
              echo " checked='checked'";
          }
          echo " />\n";
          ?><br />
          <?php
              _e('Check this box if you want upload original image file along with generated thumbnail.', 'mpu');
          ?>
        </td>
      </tr>
      <tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_THUMBNAIL_RESIZE_MODE_CFG ?>"><?php
                     _e('Resize mode', 'mpu') ?></label>
        </td>
        <td>
          <?php
          echo "<select name='".MPU_THUMBNAIL_RESIZE_MODE_CFG
               ."' id='".MPU_THUMBNAIL_RESIZE_MODE_CFG."'>\n"
               ."<option value='fit'";
          if(get_option(MPU_THUMBNAIL_RESIZE_MODE_CFG) == 'fit')
              echo " selected='selected'";
          echo ">".__('fit', 'mpu')."</option>\n"
		   ."<option value='fitByWidth'";
          if(get_option(MPU_THUMBNAIL_RESIZE_MODE_CFG) == 'fitByWidth')
              echo " selected='selected'";
          echo ">".__('fitByWidth', 'mpu')."</option>\n"
		   ."<option value='fitByHeight'";
          if(get_option(MPU_THUMBNAIL_RESIZE_MODE_CFG) == 'fitByHeight')
              echo " selected='selected'";
          echo ">".__('fitByHeight', 'mpu')."</option>\n"
		    ."<option value='exactFit'";
          if(get_option(MPU_THUMBNAIL_RESIZE_MODE_CFG) == 'exactFit')
              echo " selected='selected'";
          echo ">".__('exactFit', 'mpu')."</option>\n"
               ."<option value='stretch'";
          if(get_option(MPU_THUMBNAIL_RESIZE_MODE_CFG) == 'stretch')
              echo" selected='selected'";
          echo ">".__('stretch', 'mpu')."</option>\n"
               ."<option value='crop'";
          if(get_option(MPU_THUMBNAIL_RESIZE_MODE_CFG) == 'crop')
              echo" selected='selected'";
          echo ">".__('crop', 'mpu')."</option>\n"      
               ."</select>";
          ?><br />
          <?php _e('Resize mode for tumbnails generation. Available values: <br/>

<b>fit</b> - MultiPowUpload will generate thumbnails with size based on original image aspect ratio. result thumbnail dimensions may differs from thumbnail.width and thumbnail.height if aspect ration of original image and destionation thumbnail are different.<br/>
<b>fitByWidth</b> - The result thumbnail will always have specified width. The result thumbnails height calculated with respect of the the aspect ratio of the original image.<br/>
<b>fitByHeight</b> - The result thumbnail will always have specified height. The result thumbnails width calculated with respect of the the aspect ratio of the original image.<br/>
<b>exactFit</b> - The result thumbnail will always have specified dimensions. If the aspect rato of the original image and the destination thumbnail are different, "unused" space of thumbnail is filled with "black" color.<br/>
<b>stretch</b> - Result thumbnail will always have specified dimensions. If needed image stretched by width or height.<br/>

<b>crop</b> - Result thumbnail will always have specified dimensions. If needed, image cropped by width or height.', 'mpu'); ?>
        </td>
      </tr>
   
		<tr>
		<td width="30%" valign="top" style="padding-top: 10px;">
		  <label for="<?php echo MPU_THUMBNAIL_ALLOW_CROP_CFG ?>"><?php
					 _e('Allow crop', 'mpu') ?></label>
		</td>
		<td>
		  <?php
		  echo "<input type='checkbox' name='".MPU_THUMBNAIL_ALLOW_CROP_CFG
			   ."' id='".MPU_THUMBNAIL_ALLOW_CROP_CFG."' value='true'";
		  if (get_option(MPU_THUMBNAIL_ALLOW_CROP_CFG) == 'true') {
			  echo " checked='checked'";
		  }
		  echo " />\n";
		  ?><br />
		  <?php
			  _e('Check this box if you want to allow crop operation before upload. Applyed only for thumbnails.', 'mpu');
		  ?>
		</td>
	  </tr>
	  <tr>
		<td width="30%" valign="top" style="padding-top: 10px;">
		  <label for="<?php echo MPU_THUMBNAIL_ALLOW_ROTATE_CFG ?>"><?php
					 _e('Allow rotation', 'mpu') ?></label>
		</td>
		<td>
		  <?php
		  echo "<input type='checkbox' name='".MPU_THUMBNAIL_ALLOW_ROTATE_CFG
			   ."' id='".MPU_THUMBNAIL_ALLOW_ROTATE_CFG."' value='true'";
		  if (get_option(MPU_THUMBNAIL_ALLOW_ROTATE_CFG) == 'true') {
			  echo " checked='checked'";
		  }
		  echo " />\n";
		  ?><br />
		  <?php
			  _e('Check this box if you want to allow rotate image thumbnails. Applyed only for thumbnails.', 'mpu');
		  ?>
		</td>
	  </tr>
	  <!-- thumbnail watermark configuration  -->
	  <tr>
        <th width="30%" valign="top" style="padding-top: 12px;">
          <?php _e('Thumbnails watermark options', 'mpu'); ?>
        </th>
        <td>          
        </td>
	  <tr>
		<td width="30%" valign="top" style="padding-top: 10px;">
		  <label for="<?php echo MPU_THUMBNAIL_WATERMARK_ENABLED_CFG ?>"><?php
					 _e('Add watermark', 'mpu') ?></label>
		</td>
		<td>
		  <?php
		  echo "<input type='checkbox' name='".MPU_THUMBNAIL_WATERMARK_ENABLED_CFG
			   ."' id='".MPU_THUMBNAIL_WATERMARK_ENABLED_CFG."' value='true'";
		  if (get_option(MPU_THUMBNAIL_WATERMARK_ENABLED_CFG) == 'true') {
			  echo " checked='checked'";
		  }
		  echo " />\n";
		  ?><br />
		  <?php
			  _e('Check this box if you want to add watermark to image thumbnails.', 'mpu');
		  ?>
		</td>
	  </tr>
	   <tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_THUMBNAIL_WATERMARK_TEXT_CFG ?>"><?php _e('Watermark text', 'mpu'); ?></label>
        </td>
        <td>
          <?php
          echo "<input type='text' size='20' ";
          echo "name='".MPU_THUMBNAIL_WATERMARK_TEXT_CFG."' ";
          echo "id='".MPU_THUMBNAIL_WATERMARK_TEXT_CFG."' ";
          echo "value='".get_option(MPU_THUMBNAIL_WATERMARK_TEXT_CFG)."' />";
          echo "\n";
          ?><br />
          <?php _e('Text for watermark. Used only if value of url to imagewatermark . ', 'mpu'); ?>
        </td>
      </tr>
	   <tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_THUMBNAIL_WATERMARK_URL_CFG ?>"><?php _e('Watermark image url', 'mpu'); ?></label>
        </td>
        <td>
          <?php
          echo "<input type='text' size='50' ";
          echo "name='".MPU_THUMBNAIL_WATERMARK_URL_CFG."' ";
          echo "id='".MPU_THUMBNAIL_WATERMARK_URL_CFG."' ";
          echo "value='".get_option(MPU_THUMBNAIL_WATERMARK_URL_CFG)."' />";
          echo "\n";
          ?><br />
          <?php _e('Url to image file wich should be used as watermark .  ', 'mpu'); ?>
        </td>
      </tr>

	  <tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_THUMBNAIL_WATERMARK_ALPHA_CFG ?>"><?php _e('Watermark alpha', 'mpu'); ?></label>
        </td>
        <td>
          <?php
          echo "<input type='text' size='4' ";
          echo "name='".MPU_THUMBNAIL_WATERMARK_ALPHA_CFG."' ";
          echo "id='".MPU_THUMBNAIL_WATERMARK_ALPHA_CFG."' ";
          echo "value='".get_option(MPU_THUMBNAIL_WATERMARK_ALPHA_CFG)."' />";
          echo "\n";
          ?><br />
          <?php _e('Watermark alpha. Range from 0.0. to 1.0. ', 'mpu'); ?>
        </td>
      </tr>
	  
	   <tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_THUMBNAIL_WATERMARK_POSITION_CFG ?>"><?php
                     _e('Watermark position', 'mpu') ?></label>
        </td>
        <td>
          <?php
          echo "<select name='".MPU_THUMBNAIL_WATERMARK_POSITION_CFG
               ."' id='".MPU_THUMBNAIL_WATERMARK_POSITION_CFG."'>\n"
               ."<option value='top.left'";
          if(get_option(MPU_THUMBNAIL_WATERMARK_POSITION_CFG) == 'top.left')
              echo " selected='selected'";
          echo ">".__('top.left', 'mpu')."</option>\n"
               ."<option value='top.center'";
          if(get_option(MPU_THUMBNAIL_WATERMARK_POSITION_CFG) == 'top.center')
              echo" selected='selected'";
          echo ">".__('top.center', 'mpu')."</option>\n"
               ."<option value='top.right'";
          if(get_option(MPU_THUMBNAIL_WATERMARK_POSITION_CFG) == 'top.right')
              echo" selected='selected'";
          echo ">".__('top.right', 'mpu')."</option>\n" 
		       ."<option value='center.left'";
          if(get_option(MPU_THUMBNAIL_WATERMARK_POSITION_CFG) == 'center.left')
              echo " selected='selected'";
          echo ">".__('center.left', 'mpu')."</option>\n"
               ."<option value='center.center'";
          if(get_option(MPU_THUMBNAIL_WATERMARK_POSITION_CFG) == 'center.center')
              echo" selected='selected'";
          echo ">".__('center.center', 'mpu')."</option>\n"
               ."<option value='center.right'";
          if(get_option(MPU_THUMBNAIL_WATERMARK_POSITION_CFG) == 'center.right')
              echo" selected='selected'";
          echo ">".__('center.right', 'mpu')."</option>\n" 
		   ."<option value='bottom.left'";
          if(get_option(MPU_THUMBNAIL_WATERMARK_POSITION_CFG) == 'bottom.left')
              echo " selected='selected'";
          echo ">".__('bottom.left', 'mpu')."</option>\n"
               ."<option value='bottom.center'";
          if(get_option(MPU_THUMBNAIL_WATERMARK_POSITION_CFG) == 'bottom.center')
              echo" selected='selected'";
          echo ">".__('bottom.center', 'mpu')."</option>\n"
               ."<option value='bottom.right'";
          if(get_option(MPU_THUMBNAIL_WATERMARK_POSITION_CFG) == 'bottom.right')
              echo" selected='selected'";
          echo ">".__('bottom.right', 'mpu')."</option>\n"
               ."</select>";
          ?><br />
          <?php _e('Watermark position.', 'mpu'); ?>
        </td>
      </tr>

	  <tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_THUMBNAIL_WATERMARK_TEXT_FONT_CFG ?>"><?php _e('Watermark text font', 'mpu'); ?></label>
        </td>
        <td>
          <?php
          echo "<input type='text' size='20' ";
          echo "name='".MPU_THUMBNAIL_WATERMARK_TEXT_FONT_CFG."' ";
          echo "id='".MPU_THUMBNAIL_WATERMARK_TEXT_FONT_CFG."' ";
          echo "value='".get_option(MPU_THUMBNAIL_WATERMARK_TEXT_FONT_CFG)."' />";
          echo "\n";
          ?><br />
          <?php _e('Watermark text font. ', 'mpu'); ?>
        </td>
      </tr>
	  
	   <tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_THUMBNAIL_WATERMARK_TEXT_COLOR_CFG ?>"><?php _e('Watermark text color', 'mpu'); ?></label>
        </td>
        <td>
          <?php
          echo "<input type='text' size='20' ";
          echo "name='".MPU_THUMBNAIL_WATERMARK_TEXT_COLOR_CFG."' ";
          echo "id='".MPU_THUMBNAIL_WATERMARK_TEXT_COLOR_CFG."' ";
          echo "value='".get_option(MPU_THUMBNAIL_WATERMARK_TEXT_COLOR_CFG)."' />";
          echo "\n";
          ?><br />
          <?php _e('Watermark text color. ', 'mpu'); ?>
        </td>
      </tr>
	  
	   <tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_THUMBNAIL_WATERMARK_TEXT_SIZE_CFG ?>"><?php _e('Watermark text size', 'mpu'); ?></label>
        </td>
        <td>
          <?php
          echo "<input type='text' size='20' ";
          echo "name='".MPU_THUMBNAIL_WATERMARK_TEXT_SIZE_CFG."' ";
          echo "id='".MPU_THUMBNAIL_WATERMARK_TEXT_SIZE_CFG."' ";
          echo "value='".get_option(MPU_THUMBNAIL_WATERMARK_TEXT_SIZE_CFG)."' />";
          echo "\n";
          ?><br />
          <?php _e('Watermark text size. ', 'mpu'); ?>
        </td>
      </tr>
	  
	   <tr>
        <td width="30%" valign="top" style="padding-top: 10px;">
          <label for="<?php echo MPU_THUMBNAIL_WATERMARK_TEXT_STYLE_CFG ?>"><?php _e('Watermark text style', 'mpu'); ?></label>
        </td>
        <td>
          <?php
          echo "<input type='text' size='20' ";
          echo "name='".MPU_THUMBNAIL_WATERMARK_TEXT_STYLE_CFG."' ";
          echo "id='".MPU_THUMBNAIL_WATERMARK_TEXT_STYLE_CFG."' ";
          echo "value='".get_option(MPU_THUMBNAIL_WATERMARK_TEXT_STYLE_CFG)."' />";
          echo "\n";
          ?><br />
          <?php _e('Watermark text style. Vaid values: normal, bold, italic, underline.<br/>
You can combine styles like this: "bold, italic" ', 'mpu'); ?>
        </td>
      </tr>
    
    </table>

  </fieldset>
  
  <input type="hidden" name="action" value="update" />
  <p><input type="submit" name="Submit" value="<?php _e('Update Options', 'mpu'); ?>" /></p>

</div>
</form>
<?php
}

?>