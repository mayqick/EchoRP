var messaging = {
  scroll_to_bottom : function(){
    // Scrolls right to bottom of messages
    if($('#messaging ul').length > 0)
      $('#messaging ul').get(0).scrollTop = $('#messaging ul').get(0).scrollHeight;
  }
};

$('#messaging input').bind('focus', function(e){
  
  e.stopPropagation();
  
  $('#messaging').addClass('focused');
  $('#inventory,#character').removeClass('focused');
  
  $('.preview').attr('src', 'https://s3-us-west-2.amazonaws.com/s.cdpn.io/109682/adultlink.png');
  
});

$('#messaging').bind('mousedown', function(e){
  
  if($(this).hasClass('focused')){
    e.stopPropagation();
  }
  
});

messaging.scroll_to_bottom();

$('#inventory,#character').mousedown(function(e){
  
  e.stopPropagation();
  
  if($(this).attr('id') == "character" && !$(this).hasClass('focused')){
    $('.preview').attr('src', 'https://s3-us-west-2.amazonaws.com/s.cdpn.io/109682/adultlink.gif');
  }
  
  if($(this).attr('id') == "inventory"){
    $('.preview').attr('src', 'https://s3-us-west-2.amazonaws.com/s.cdpn.io/109682/adultlink.png');
  }
  
  $('#inventory,#character').removeClass('focused');  
  $('#messaging').removeClass('focused');
  $(this).addClass('focused');
});

var grabbed = null;

$('#inventory,#character').click(function(e){
  e.stopPropagation();
})

$('#inventory header,#character header').mousedown(function(e){
  if(!holding){
    var closest_div = $(this).closest('div');

    grabbed = {
      element : closest_div,
      clientX : e.clientX - closest_div.offset().left,
      clientY : e.clientY - closest_div.offset().top
    }
  }
});

$('html').mouseup(function(e){
  if(grabbed)
    grabbed = null;
});

$('html').mousemove(function(e){
  if(grabbed){
    grabbed.element.css({
      left : e.clientX - grabbed.clientX,
      top : e.clientY - grabbed.clientY
    });
  }
});

$('html').bind('mousedown', function(e){
  
  $('#messaging').removeClass('focused');
  messaging.scroll_to_bottom();
  $('#inventory,#character').removeClass('focused');
  
  $('.preview').attr('src', 'https://s3-us-west-2.amazonaws.com/s.cdpn.io/109682/adultlink.png');
  
});

$('button.close').mousedown(function(){
  $(this).closest('div').addClass('closed');
})

messaging.scroll_to_bottom();

// Great model rip... shame downloads gone...
// http://www.vg-resource.com/thread-19690.html


// I am looking for a modeler by the way.

// Interface from OOT
// http://i53.photobucket.com/albums/g74/rikku300/Uni/FYP/N64Ocarinaoftime4menuscreens.png:original

$('html').keydown(function(e){
  
  //c 67
  if(e.keyCode == 67)
    $('#character').toggleClass('closed');
  
  if(e.keyCode == 73)
  //i 73
    $('#inventory').toggleClass('closed');
});

var holding = null,
    dropInto = undefined;

$('#inventory .item, #character .item').mousedown(function(e){
    
  var from = "";
  if($(this).parents('#inventory').length > 0)
    from = "inventory";
  if($(this).parents('#character').length > 0)
    from = "character";
  
  holding = {
    element: $(this),
    from: from,
    offsetInElementX: (e.clientX - $(this).offset().left) * 1.1,
    offsetInElementY: (e.clientY - $(this).offset().top) * 1.1,
    clientX: e.clientX,
    clientY: e.clientY
  }
})

$(window).bind('mousemove', function(e){
  
  if(holding && !grabbed && (Math.abs(e.clientX - holding.clientX) > 10 ||
  Math.abs(e.clientY - holding.clientY) > 10)){
    
    holding.element.addClass('held');
    
    $('#holding').html(holding.element.clone().removeClass('held'))
    
    $('#holding').addClass('show');
    
    $('#holding').css({
      //position: "fixed",
      left : e.clientX - holding.offsetInElementX,
      top : e.clientY - holding.offsetInElementY
    })
        
  }
})

$(window).bind('mouseup', function(){
  
  if(holding){
    
    if(dropInto)
      alert(JSON.stringify(dropInto))
    
    dropInto = null;
    
    holding.element.removeClass('held'); holding.element.parents('li').removeClass('highlight');
    
    $('#holding').html("");
    
    $('#holding').removeClass('show');
    
    holding = null;
  }
})

$('#inventory .item, #character .item').mouseenter(function(){
  if(!holding){
    $(this).addClass('highlight');
  }
})

$('#inventory .item, #character .item').mouseleave(function(){
  $(this).removeClass('highlight');
})

$('#inventory li, #character li').mouseenter(function(){
  if(holding){
    
    var to = "";
    if($(this).parents('#inventory').length > 0)
      to = "inventory";
    if($(this).parents('#character').length > 0)
      to = "character";
    
    dropInto = {
      to: to,
      from: holding.from,
      slot: 
        to == "character" ? 
            $(this).attr('id') :
            $(this).index()
    }
    
    console.log(dropInto)
    
    $(this).addClass('highlight');
  }
})

$('#inventory li, #character li').mouseleave(function(){
  $(this).removeClass('highlight');
  dropInto = null;
})