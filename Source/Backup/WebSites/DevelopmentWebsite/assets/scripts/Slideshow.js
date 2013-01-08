(function(jQuery){
jQuery.fn.extend({
  SplitID : function()
  {
    return this.attr('id').split('-').pop();
  },

  Slideshow : {
    Ready : function()
    {
      this.timeoutid = setInterval('jQuery.fn.Slideshow.Transition();', 10000);
      jQuery.fn.Slideshow.afterInterrupt = false;
      this.Counter = 1;
      this.Interrupted = false;
      this.Transition(); 
           
      
      jQuery('.yt-animationBulletsBlock li') //to handle the hover effect of the bullets
        .hover(
          function() { //mouseOver
            jQuery.fn.Slideshow.Interrupted = true;
            clearInterval(jQuery.fn.Slideshow.timeoutid); //Interrupting the slideshow
    
            jQuery('div.yt-slide').css('display','none');//hide all the slides at once
            jQuery('.yt-animationBulletsBlock li').removeClass('currentSlideBullet');//remove the currentSlideBullet class from all the bullets

            jQuery('div#yt-slide-' + jQuery(this).SplitID()).show(); //show the respective slide
            jQuery(this).addClass('currentSlideBullet'); //show the respective bullet
          },
          function(){ //mouseOut 
             jQuery.fn.Slideshow.Interrupted = false;
             jQuery.fn.Slideshow.Counter = jQuery('li.currentSlideBullet').SplitID();
             jQuery.fn.Slideshow.afterInterrupt = true; 
             
             jQuery.fn.Slideshow.timeoutid = setInterval('jQuery.fn.Slideshow.Transition();', 10000); //restarting the slideshow   
          }
        );

      jQuery('a.slideTab') //to handle the hover effects on the side tabs
      .hover(
          function() { //mouseOver
            jQuery.fn.Slideshow.Interrupted = true;
            clearInterval(jQuery.fn.Slideshow.timeoutid); //Interrupting the slideshow

            jQuery('div.yt-slide').css('display','none');
            jQuery('.yt-animationBulletsBlock li').removeClass('currentSlideBullet');

            jQuery('div#yt-slide-' + jQuery(this).SplitID()).show();
            jQuery('li#slideBullet-' + jQuery(this).SplitID()).addClass('currentSlideBullet');            
          },
          function(){ //mouseOut
             jQuery.fn.Slideshow.Interrupted = false;
             jQuery.fn.Slideshow.Counter = jQuery('li.currentSlideBullet').SplitID();
             jQuery.fn.Slideshow.afterInterrupt = true;
             
             jQuery.fn.Slideshow.timeoutid = setInterval('jQuery.fn.Slideshow.Transition();', 10000); //restarting the slideshow 
          }
        );        
    },

    Transition : function()
    {
      if (!this.Interrupted){ //slideshow will be run only if interrupt is false
      
          if(jQuery.fn.Slideshow.afterInterrupt == true){
              jQuery.fn.Slideshow.Counter++;

              if (jQuery.fn.Slideshow.Counter > 7) {
                jQuery.fn.Slideshow.Counter = 1;
              }
              jQuery.fn.Slideshow.afterInterrupt = false;
          }
          this.Last = this.Counter - 1;

          if (this.Last < 1) {
            this.Last = 7;
          }

          jQuery('div#yt-slide-' + this.Last).fadeOut(
            'fast',
            function() {
                
                      jQuery('li#slideBullet-' + jQuery.fn.Slideshow.Last).removeClass('currentSlideBullet');
                      jQuery('li#slideBullet-' + jQuery.fn.Slideshow.Counter).addClass('currentSlideBullet');
                      jQuery('div#yt-slide-' + jQuery.fn.Slideshow.Counter).fadeIn('slow');

              
              jQuery.fn.Slideshow.Counter++;
              
              if (jQuery.fn.Slideshow.Counter > 7) {
                jQuery.fn.Slideshow.Counter = 1;
              }
            }        
          );
      }
    }
  }
});

jQuery(document).ready(
  function() {
    jQuery.fn.Slideshow.Ready();
  }
);

})(jQuery); 