// phat trien boi ducluanmaster@gmail.com 0948080247
	$('#btn-switch').click(function () {
    //    var diem_den = $('#depAirport').html(); 
    //    var diem_di = $('#arvAirport').html();   
       // $('.bt-diem-den').html(diem_di);
       // $('.bt-diem-di').html(diem_den); 
	var diem_di_text = document.getElementById("depAirport").value;  
	var diem_den_text = document.getElementById("arvAirport").value; 
	 
	 	//alert(diem_di_text);
	 	//alert(diem_den_text);
		//document.getElementById('depAirport').value = diem_den_text;​​​​​​​​​​
		//$('#depAirport').html(diem_di_text);
		 
		document.getElementById("depAirport").value  = diem_den_text;	 
		document.getElementById("arvAirport").value = diem_di_text;
 
    });		
var App = {
	init_complete : false,	
	plugin : {
        accounting : accounting,
		rslide : function(elm, option) {
			$(elm).responsiveSlides(option);
		},
		carousel : function(elm){
			$(elm).owlCarousel(elm);
		},
		/*bxslider: function(elm){
			$(elm).bxSlider({
				auto: true,
				controls: false,
				buildPager: function(slideIndex){
					return slideIndex+1;
			  }
			});
		}*/
    },
	
	init : function(){
		this.dotdotdot();
		this.plugin.accounting.settings = {
			number: {
				precision: 0, // default precision on numbers is 0
				thousand: ".", decimal: ","
			},
			currency: {
				symbol: "VNĐ",
				thousand: ".",
				format: "%v %s",
				precision: 0
			}
		};
		$('[role="carousel"]').each(function(){
			var option = $(this).data('option');
			App.plugin.carousel(this);
		});
		//this.plugin.bxslider('.bxslider');
		//this.plugin.rslide('.rslides',{pager: false});
		
		this.book_form.init();
		this.format_money('.currency');
		if(typeof($.fn.validate)!='undefined'){
			this.form_validate();
		}
		
		
		//Navbar dropdown open on hover
		$('.navbar-nav').find('.dropdown').hover(
			function() {
				$(this).addClass('open');
			},
			function() {
				$(this).removeClass('open');
			}
		);
		
		//Resize image
		if(typeof($.fn.fancybox)!='undefined'){
			$(window).load(function(){
				var cImg = new Image();
				var maxWidth = $('.post-content').width();
				$('.post-content').find('img').each(function(){
					cImg.src= $(this).attr('src');
					if(cImg.width>maxWidth){
						$(this).wrap("<a rel='img-in-post' class=\"post-thumb-img\" href=\""+cImg.src+"\"></a>");
					}
				});
				$(".post-thumb-img").fancybox({
					prevEffect: 'elastic',
					nextEffect: 'elastic',
					openEffect: 'elastic',
					closeEffect: 'elastic',
					helpers		: {
						buttons	: {}
					}        
				});
			});
		}
		
		
		$('#copy-name').click(function(){
			if($('#fullname').val().length){
				$('input[name="passenger[0]"]').val($('#fullname').val());
			}
		});
		
		$('.stop-point').find('.show-detail').children('a').click(function () {
			$(this).closest('.stop-point').children('.detail').toggle();        
		});
		
		this.init_complete = true;
		
	},
	
	format_money : function(elm){
         var app = this;
		 $(elm).each(function() {        
            $(this).text(app.plugin.accounting.formatMoney($(this).text()));
        });		
    },
	
	book_form : {
		airport_field : null,		
		list_city : null,
		list_city_open : true,
		list_city: null,
		init : function(){
			$('.sig-down, .sig-down2').click(function(e){
				e.preventDefault();
				$(this).prev().focus();
			});
			var bf = this;					
		//	this._create_dialog('#listCity');	
			if(typeof($.fn.datepicker)!='undefined'){
				this._create_datepicker();
			}
			
			this._number_ticker();
			this._load_cookie_data();			
			//Bind Events
			/*
			$('input[name="flightType"]').change(
					function() {
						if ($(this).is(':checked')) {
							$("#rtnDate").prop('disabled', false);
						} else {
							$("#rtnDate").val('');
							$("#rtnDate").prop('disabled', true);
						}
					}
			);*/
			// dlp 2015
			$('input:radio[name="flightType"]').change(
					function() {
						if ($(this).val() > 0) {
							$("#rtnDate").prop('disabled', false);
							// valid_return_date();
							//$("#rtnDate").focus(); //
							//alert('1');
						} else {
						//	$("#rtnDate").val('');
							$("#rtnDate").prop('disabled', true);
							//alert('2');
						}
					}
			);
			$('#rtnDate').click(function(e) {
				//alert('3'); 
				$("input[id='flightType1']").prop('checked',false);
				$("input[id='flightType']").prop('checked',true); 
			});
			
			
			/*  bat chon sanbaykieu cu*/
			$('#depAirport, #arvAirport').on('click focus',function() {
				bf.airport_field = this;	
				bf._list_city_handler();				
			});	 
			$('#listCity .list-group-item').click(function(e) {
				bf._select_airport_handler(this);
			});
			// dlp
			$('#btnChooseLocation').bind('click', function () {
				var location = $('#inter-city-departure');
				var loc = location.val();
				if (location.prop('tagName') == 'SELECT') {
					if (location.val() == null || location.val() == '-1')
						return;
					loc = $('#inter-city-departure option:selected').text();
					choseLocationFromDialog = true;
				}

				if (loc.length > 0 && choseLocationFromDialog) {
					bf._select_airport_handlerx(this);
				  
				//	var element = $('#departure-location-dlg').attr('data-id');
				//	$('#' + element).val(loc);
					//var valuedElement = element.split('-')[0];
				//	var locationId = loc.substring(loc.indexOf('(') + 1, loc.lastIndexOf(')'));
					//$('#' + valuedElement).val(locationId);
					//$('#inter-city-departure').val('');
				}
				// $dialog.dialog('close');
				//$('#listCity').modal('hide'); 
			});
			
			$('.search-form input[type!="radio"]').on('change',function(){
				bf._auto_focus(this);				
			});	 
			if(typeof($.fn.autocomplete)!='undefined'){
				$(".airport_search").autocomplete({
					source: function(request, response) {
						$.ajax({
							url: base_url + "service/airport/" + request.term,
							dataType: "json",
							success: function(data) {
								response($.map(data, function(item) {
									return {
										label: item.city_name + ' (' + item.code + ')' + ', ' + item.country_name
									}
								}));
							}
						});
					},
					minLength: 2,
					//Gan gai tri vao textbox khi chon tu autocomplete list
					select: function(event, ui) {
						$(bf.airport_field).val(ui.item.label);
						$('#listCity').modal('hide');
						$(bf.airport_field).trigger('change');;
					},
					close: function(event, ui) {
						$(this).val('');
						
					},
					
				});
			}
			
			$('.list-item-toggle').click(function(){
				$(this).closest('.list-group').children('.list-group-item').toggle();
			});
			
			
		},
		_number_ticker: function(){
			$('.sign').click(function(){
				var target = $(this).parent().children('input') || false;
				if(target){
					var _bonus = 0;
					if($(this).hasClass('plus')){
						_bonus = 1;
					}else if($(this).hasClass('minus')){
						_bonus = -1;
					}
					target.val(parseInt(target.val())+_bonus);
					if(target.val()<1){
						target.val(0);
					}
				}
				
			});			
			$('.signa').click(function(){
				var target = $(this).parent().children('input') || false;
				if(target){
					var _bonus = 0;
					if($(this).hasClass('plus')){
						_bonus = 1;
					}else if($(this).hasClass('minus')){
						_bonus = -1;
					}
					target.val(parseInt(target.val())+_bonus);
					if(target.val()<1){
						target.val(1);
					}
				}
				
			});
		},
		/*_load_cookie_data: function(){
			if($.cookie('flight_type')==0){				
				$("input[id='flightType1']").prop('checked',true);
				$("input[id='flightType']").prop('checked',false);
			}else{
				
				$("input[id='flightType1']").prop('checked',false);
				$("input[id='flightType']").prop('checked',true);
				this._toogle_arv_field();
			}
			if($.cookie('dep_airport')){
				$('#depAirport').val($.cookie('dep_airport'));
			}
			if($.cookie('arv_airport')){
				$('#arvAirport').val($.cookie('arv_airport'));
			}
			$('#adultNo').val($.cookie('adult_no') || 1).change();
			$('#childNo').val($.cookie('child_no') || 0).change();
			$('#infantNo').val($.cookie('infant_no') || 0).change();
		},
		*/
		
		
		_load_cookie_data: function(){
			/*
			if($.cookie('flight_type')==1){				
				$("input[name='flightType']").prop('checked',true)
			}else{
				$("input[name='flightType']").prop('checked',false)
				this._toogle_arv_field();
			}
			*/			
			if($.cookie('flight_type')=='on'){				
				$('input:radio[value="' + '0'+ '"]').trigger('click');
			}
			// dlp 2015
			$('input:radio[value="' + $.cookie('flight_type') + '"]').trigger('click');
			
			
			if($.cookie('flight_type')=='1'){
				$("#rtnDate").prop('disabled', false);
			} else {
				$("#rtnDate").prop('disabled', true);
			}
	
			/*if ($('#flightType').is(':checked') === false) {
				$('#flightType').filter('[value=' + $.cookie('flightType') + ']').prop('checked', true);
			}*/
			
			if($.cookie('dep_airport')){
				$('#depAirport').val($.cookie('dep_airport'));
			}
			if($.cookie('arv_airport')){
				$('#arvAirport').val($.cookie('arv_airport'));
			}
			$('#adultNo').val($.cookie('adult_no') || 1).change();
			$('#childNo').val($.cookie('child_no') || 0).change();
			$('#infantNo').val($.cookie('infant_no') || 0).change();
		},
		_toogle_arv_field : function(){
			 
			 $("#rtnDate").prop('disabled', ! $("input[name='flightType']").is(':checked'));
		},
		_create_dialog: function(selector){
			$(selector).dialog({
			  autoOpen: false,
			 
             maxHeight: 300,
			  draggable: false,
			 modal: true,
			  show: {
				effect: "blind",
				duration: 300
			  },
			  open: function(event,ui){
				console.log('o');
				$('.ui-widget-overlay').bind('click', function(){ $(selector).dialog( "close" ); });
			  },
			  close: function(event,ui){
				console.log('c');
				App.book_form.list_city_open = true;
			  },
			  beforeClose: function( event, ui ) {
				  console.log('bc');
			  }
			});
			this.list_city = $(selector).dialog( "instance" );
		},		
		_create_datepicker: function(){
			 var bf = this;
			 $('.child_birthday').datepicker({
				dateFormat: 'dd/mm/yy',
				changeYear: true,
				changeMonth: true,
				minDate: '-13y -1d',
				maxDate: '-2y -1d'
			});
			 $('.infant_birthday').datepicker({
				dateFormat: 'dd/mm/yy',
				changeYear: true,
				changeMonth: true,
				minDate: '-2y -1d',
				maxDate: '0d'
			});
			
			$( "#depDate, #rtnDate" ).datepickerlunar({     
			  dateFormat: 'dd/mm/yy',
			  numberOfMonths: 2,
			  minDate: 0,
			  maxDate: "+11m",
			  changeMonth: true,
			  beforeShow: function(input, inst){
				  inst.dpDiv.removeClass('ui-helper-hidden-accessible');
				  if($(input).prop('disabled')){
					  return false;
				  }
			  },
			  onSelect: function(datetext, inst){
				bf._valid_return_date();
			  },
			  onClose: function(dateText, inst) {
				  $(this).trigger('change');
				  

			  }
			});
		},
		_valid_return_date: function () {
			var date_segment = $('#depDate').val().split('/');
			var depDate = new Date(date_segment[2], date_segment[1] - 1, date_segment[0]);
			if (!$('#rtnDate').prop('disabled')) {
				$('#rtnDate').datepickerlunar("option", "minDate", depDate);
				return true;
			}
			return false;
		},
		_list_city_handler: function(){
			/*if(App.book_form.list_city.isOpen()===false){
				if(App.book_form.list_city_open){
					App.book_form.list_city_open = false;
					App.book_form.list_city.option("title", 'Chọn ' + $(App.book_form.airport_field).data('dialog-title') );
					//app.book_form.list_city.option("position", { my: "left top", at: "left+34.5 top+"+$(app.book_form.airport_field).offset().top, of: '#wrapper' })																	
					App.book_form.list_city.option("position", { my: "left top", at: "left top", of: App.book_form.airport_field })																	
					setTimeout(function(){
						App.book_form.list_city.open();		
					},250);
					
					
				}else{
					App.book_form.list_city.close();						
				}
			}*/
			/*Bootstrap version
			**********************/
			$('#listCity').modal('show');
			$('#listCityTitle').html('Chọn điểm ' + $(this.airport_field).data('dialog-title') );
			
		},
		_select_airport_handler : function(elm){
			//Gan gia tri vao text box
			/*if ($(elm).children('a').data('airportcode') === '')
				return false;
			App.book_form.list_city.close();
			$(App.book_form.airport_field).val($(elm).children('a').text()).trigger('change');	
			*/
			
			if ($(elm).children('a').data('airportcode') === '')
				return false;
			$('#listCity').modal('hide');
			$(this.airport_field).val($(elm).children('a').text()).trigger('change');										
			
		},
		_select_airport_handlerx : function(elm){
			//Gan gia tri vao text box dlp
			if ($(elm).children('a').data('airportcode') === '')
				return false;
			$('#listCity').modal('hide');
			$(this.airport_field).val($('#inter-city-departure option:selected').text()).trigger('change');
			
		},
		_auto_focus: function(elm){
			var next = $(elm).data('next') || false;
			if(next){
				setTimeout(function(){
					$(next).focus();
				},200);
			}				
		}
	},
	lazyload : function(){
		$(window).load(function(){
			$('#loading-screen').remove();
			$('body').css('overflow','auto');
		});
	},
	
	form_validate : function(){		
		$(".search-form form").validate({
			messages : {
				'rtnDate' : 'Vui lòng chọn ngày về'
			}			
		});
		$("#frmSubcribe").validate({
			rules:{
				'email': {
					required: true,
					email : true
				}
			},
			messages:{
				'email': {
					required: 'Vui lòng điền email của bạn.',
					email: 'Địa chỉ email không hợp lệ.'
				}
			},
			errorLabelContainer: $("#frmSubcribe div.error"),
			error: 'text-danger'
					
		});
	},
	dotdotdot : function(){
		if(typeof($.fn.dotdotdot)=='undefined') return false;
		$('.description').dotdotdot();
		$('[data-toggle="tab"]').on('shown.bs.tab', function (e) {
			$($(e.target).attr('href')).find('.description').dotdotdot();
		})
	}
}
$(document).ready(function(){
    App.init();    
	    $(document).on('change', 'input[Id="displayPriceType1"]', function (e) {
        //alert($(this).val()); 
        if( $('.chuathuephi').hasClass('hien1') ) {  
            //    $this.addClass('header-sticky'); 
            //    $this.removeClass('header-sticky');    
			//document.getElementById("chuathuephi").classList.remove("hien1");
		//	$('.chuathuephi').removeClass( 'hien1' );
		//	$('.chuathuephi').removeClass( 'hien1' );
		//	$('.chuathuephi').removeClass( 'hien1' );
        }else{
			$('.chuathuephi').addClass( 'hien1' );
			$('.giathuephi').removeClass( 'hien1' );
			$('.tonggiathuephi').removeClass( 'hien1' );
			//document.getElementById("chuathuephi").classList.add("hien1");
		} 
    
    });  
  
    $(document).on('change', 'input[Id="displayPriceType2"]', function (e) {
        //alert($(this).val()); 
        if( $('.giathuephi').hasClass('hien1') ) {   
        }else{
			$('.giathuephi').addClass( 'hien1' );
			$('.chuathuephi').removeClass( 'hien1' );
			$('.tonggiathuephi').removeClass( 'hien1' ); 
		} 
    });
 
    $(document).on('change', 'input[Id="displayPriceType3"]', function (e) {
        //alert($(this).val()); 
        if( $('.tonggiathuephi').hasClass('hien1') ) {   
        }else{
			$('.tonggiathuephi').addClass( 'hien1' );
			$('.chuathuephi').removeClass( 'hien1' );
			$('.giathuephi').removeClass( 'hien1' ); 
		} 
    });
	
	
});
 