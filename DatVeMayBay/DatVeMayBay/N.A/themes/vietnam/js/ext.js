$(document).ready(function(){
  $("#watch").click(function(){
    $("#wrap3").slideDown();
    return false;
  })
})

     function getThu(julius){  
        var numberThu = julius % 7;
        var thu;
        switch (numberThu)
        {
          case 0:
              thu = 'Thứ Hai';
              break;
           case 1:
              thu = 'Thứ Ba';
              break;          
          case 2:
              thu = 'Thứ Tư';
              break;
          case 3:
              thu = 'Thứ Năm';
              break;
          case 4:
              thu = 'Thứ Sáu';
              break;
          case 5:
              thu = 'Thứ Bảy';
              break;
          case 6:
              thu = 'Chủ Nhật';
              break;  
              
                         
        }
        
        return thu;
       
    }  
    
     function getCanChi(year){  
       
        var can = (year+6) % 10;
        var chi = (year+8) % 12;
        var canchi;
        switch (can)
        {
          case 0:
              can1 = 'Giáp';
              break;
           case 1:
              can1 = 'Ất';
              break;          
          case 2:
              can1 = 'Bính';
              break;
          case 3:
              can1 = 'Đinh';
              break;
          case 4:
              can1 = 'Mậu';
              break;
          case 5:
              can1 = 'Kỷ';
              break;
          case 6:
              can1 = 'Canh';
              break;  
          case 7:
              can1 = 'Tân';
              break; 
          case 8:
              can1 = 'Nhâm';
              break;
          case 9:
              can1 = 'Quý';
              break;   
                         
        }
        
        switch (chi)
        {
          case 0:
              chi1 = 'Tý';
              break;
          case 1:
              chi1 = 'Sửu';
              break;          
          case 2:
              chi1 = 'Dần';
              break;
          case 3:
              chi1 = 'Mão';
              break;
          case 4:
              chi1 = 'Thìn';
              break;
          case 5:
              chi1 = 'Tỵ';
              break;
          case 6:
              chi1 = 'Ngọ';
              break;  
          case 7:
              chi1 = 'Mùi';
              break; 
          case 8:
              chi1 = 'Thân';
              break;
          case 9:
              chi1 = 'Dậu';
              break;   
          case 10:
              chi1 = 'Tuất';
              break; 
          case 11:
              chi1 = 'Hợi';
              break; 
              
        }
        canchi = can1 + ' ' + chi1;
        
        
        return canchi;
       
    }
	    function jdFromDate(dd,mm,yy){       
        var a, y, m, jd;
        a = parseInt((14 - mm) / 12); 
            
        y = yy+4800-a;        
        m = mm+12*a-3;
        jd = dd + parseInt((153*m+2)/5) + 365*y + parseInt(y/4) - parseInt(y/100) + parseInt(y/400) - 32045;
        if (jd < 2299161) {
        	jd = dd + parseInt((153*m+2)/5) + 365*y + parseInt(y/4) - 32083;
        }       
        return jd;
    }  
    function getNewMoonDay(k,timeZone){       
        var T, T2, T3, dr, Jd1, M, Mpr, F, C1, deltat, JdNew, PI = 3.14159265359;
        T = k/1236.85; 
        T2 = T * T;
        T3 = T2 * T;
        dr = PI/180;
       
        Jd1 = 2415020.75933 + 29.53058868*k + 0.0001178*T2 - 0.000000155*T3;
        Jd1 = Jd1 + 0.00033*Math.sin((166.56 + 132.87*T - 0.009173*T2)*dr); 
        M = 359.2242 + 29.10535608*k - 0.0000333*T2 - 0.00000347*T3; 
        Mpr = 306.0253 + 385.81691806*k + 0.0107306*T2 + 0.00001236*T3;  
        F = 21.2964 + 390.67050646*k - 0.0016528*T2 - 0.00000239*T3; 
        C1=(0.1734 - 0.000393*T)*Math.sin(M*dr) + 0.0021*Math.sin(2*dr*M);
        C1 = C1 - 0.4068*Math.sin(Mpr*dr) + 0.0161*Math.sin(dr*2*Mpr);
        C1 = C1 - 0.0004*Math.sin(dr*3*Mpr);
        C1 = C1 + 0.0104*Math.sin(dr*2*F) - 0.0051*Math.sin(dr*(M+Mpr));
        C1 = C1 - 0.0074*Math.sin(dr*(M-Mpr)) + 0.0004*Math.sin(dr*(2*F+M));
        C1 = C1 - 0.0004*Math.sin(dr*(2*F-M)) - 0.0006*Math.sin(dr*(2*F+Mpr));
        C1 = C1 + 0.0010*Math.sin(dr*(2*F-Mpr)) + 0.0005*Math.sin(dr*(2*Mpr+M));
        if (T < -11) {
        	deltat= 0.001 + 0.000839*T + 0.0002261*T2 - 0.00000845*T3 - 0.000000081*T*T3;
        } else {
        	deltat= -0.000278 + 0.000265*T + 0.000262*T2;
        };
        JdNew = Jd1 + C1 - deltat;       
        return parseInt(JdNew + 0.5 + timeZone/24)
    }    
    function getLunarMonth11(yy,timeZone){       
        var k, off, nm, sunLong;
        off = jdFromDate(31, 12, yy) - 2415021;
        k = parseInt(off / 29.530588853);
        nm = getNewMoonDay(k, timeZone);
        sunLong = getSunLongitude(nm, timeZone); // sun longitude at local midnight
        if (sunLong >= 9) {
        	nm = getNewMoonDay(k-1, timeZone);
        }
        return nm;
    }   
    function getSunLongitude(jdn, timeZone){       
        var T, T2, dr, M, L0, DL, L, PI = 3.14159265359;
        T = (jdn - 2451545.5 - timeZone/24) / 36525; 
        T2 = T*T;
        dr = PI/180;
        M = 357.52910 + 35999.05030*T - 0.0001559*T2 - 0.00000048*T*T2; 
        L0 = 280.46645 + 36000.76983*T + 0.0003032*T2; 
        DL = (1.914600 - 0.004817*T - 0.000014*T2)*Math.sin(dr*M);
        DL = DL + (0.019993 - 0.000101*T)*Math.sin(dr*2*M) + 0.000290*Math.sin(dr*3*M);
        L = L0 + DL; // true longitude, degree
        L = L*dr;
        L = L - PI*2*(parseInt(L/(PI*2)));
        return parseInt(L / PI * 6)
    }   
    function getLeapMonthOffset(a11, timeZone){       
        var k, last, arc, i;
        k = parseInt((a11 - 2415021.076998695) / 29.530588853 + 0.5);
        last = 0;
        i = 1;
        arc = getSunLongitude(getNewMoonDay(k+i, timeZone), timeZone);
        do {
        	last = arc;
        	i++;
        	arc = getSunLongitude(getNewMoonDay(k+i, timeZone), timeZone);
        } while (arc != last && i < 14);
        return i-1;
    }  
    function updateAD(valuedate){
             var flightcheckin = valuedate;            
            flight = flightcheckin.split('/');             
            var dd= parseInt(flight[0]);            
            var mm= parseInt(flight[1]);
            var yy= parseInt(flight[2]);            
            var timeZone= 7;            
            var k, dayNumber, monthStart, a11, b11, lunarDay, lunarMonth, lunarYear, lunarLeap;
            dayNumber = jdFromDate(dd, mm, yy);            
            k = parseInt((dayNumber - 2415021.076998695) / 29.530588853);
            monthStart = getNewMoonDay(k+1, timeZone);
            
            if (monthStart > dayNumber) {
            	monthStart = getNewMoonDay(k, timeZone);
            }
            a11 = getLunarMonth11(yy, timeZone);          
            b11 = a11;
            if (a11 >= monthStart) {
            	lunarYear = yy;
            	a11 = getLunarMonth11(yy-1, timeZone);
            } else {
            	lunarYear = yy+1;
            	b11 = getLunarMonth11(yy+1, timeZone);
            }
            
          
            var getthu = getThu(dayNumber);
           
            
            lunarDay = dayNumber-monthStart+1;
           
            diff = parseInt((monthStart - a11)/29);
            
            lunarLeap = 0;
            lunarMonth = diff+11;
                    
            if (b11 - a11 > 345) {
            	leapMonthDiff = getLeapMonthOffset(a11, timeZone);
                       
            	if (diff >= leapMonthDiff) {
            		lunarMonth = diff + 10;
            		if (diff == leapMonthDiff) {
            			lunarLeap = 1;
            		}
            	}
            }
            if (lunarMonth > 12) {
            	lunarMonth = lunarMonth - 12;
            }
            if (lunarMonth >= 11 && diff < 4) {
            	lunarYear -= 1;
            }
            var canchi = getCanChi(lunarYear);
          
            $('div.licham').html('Âm lịch: ' + getthu + ' ' + lunarDay + '/' + lunarMonth + ' năm ' + canchi);
            $('div.lichamdesparture').html('Âm lịch: ' + getthu + ' ' + lunarDay + '/' + lunarMonth + ' năm ' + canchi);
            }
 
 function updateADreturn(valuedate){
	if (typeof valuedate !== 'undefined') {
            var flightcheckin = valuedate;            
            flight = flightcheckin.split('/');             
            var dd= parseInt(flight[0]);            
            var mm= parseInt(flight[1]);
            var yy= parseInt(flight[2]);            
            var timeZone= 7;            
            var k, dayNumber, monthStart, a11, b11, lunarDay, lunarMonth, lunarYear, lunarLeap;
            dayNumber = jdFromDate(dd, mm, yy);            
            k = parseInt((dayNumber - 2415021.076998695) / 29.530588853);
            monthStart = getNewMoonDay(k+1, timeZone);
            
            if (monthStart > dayNumber) {
            	monthStart = getNewMoonDay(k, timeZone);
            }
            a11 = getLunarMonth11(yy, timeZone);          
            b11 = a11;
            if (a11 >= monthStart) {
            	lunarYear = yy;
            	a11 = getLunarMonth11(yy-1, timeZone);
            } else {
            	lunarYear = yy+1;
            	b11 = getLunarMonth11(yy+1, timeZone);
            }
            
          
            var getthu = getThu(dayNumber);
           
            
            lunarDay = dayNumber-monthStart+1;
           
            diff = parseInt((monthStart - a11)/29);
            
            lunarLeap = 0;
            lunarMonth = diff+11;
                    
            if (b11 - a11 > 345) {
            	leapMonthDiff = getLeapMonthOffset(a11, timeZone);
                       
            	if (diff >= leapMonthDiff) {
            		lunarMonth = diff + 10;
            		if (diff == leapMonthDiff) {
            			lunarLeap = 1;
            		}
            	}
            }
            if (lunarMonth > 12) {
            	lunarMonth = lunarMonth - 12;
            }
            if (lunarMonth >= 11 && diff < 4) {
            	lunarYear -= 1;
            }
            var canchi = getCanChi(lunarYear);         
          
            $('div.lichamreturn').html('' + getthu + ' ' + lunarDay + '/' + lunarMonth + ' năm ' + canchi);
            }
	}
	  
	$('#depDate').change(function(e) { 
		 updateAD($('#depDate').val()); 
	});	 
	$('#rtnDate').change(function(e) {
		 //alert('dm thang cho nao an cap code cua bo'); 
		 updateADreturn($('#rtnDate').val()); 
	});
	
	
$(document).ready(function(){
updateAD($('#depDate').val()); 
updateADreturn($('#rtnDate').val()); 
 
}); 
	 
	 
	 
	 function valid_total_pax(main_selector, sub_selector, depen_selector, max){
    var main_range, sub_range, dep_range;
    var main_value, sub_value, dep_value;
    main_range = parseInt($(main_selector).val());
    sub_range = max-main_range;
    dep_range = main_range;
    
    $(sub_selector).empty();
    for(var i = 0; i<=sub_range; i++){
        $(sub_selector).append($('<option>'+i+'</option>').val(i));
        $.uniform.update(sub_selector);
    }
    $(depen_selector).empty();
    for(var i = 0; i<=main_range; i++){
        $(depen_selector).append($('<option>'+i+'</option>').val(i));
        $.uniform.update(depen_selector);
    }
}

$(window).scroll(function () {
	if($('#detail-holder').length){
        var distance = ($('#detail-holder').offset().top - $(window).scrollTop());					
        if(distance < 0){
            if(! $('#detail-holder').hasClass('fly')){
                $('#detail-holder').addClass('fly');
                $('#detail-holder').width("286px");
            }
        }
    } 
    if($('.flight-info, .customer-information, .search-result').length){
        if(($('.flight-info, .customer-information, .search-result').eq(0).offset().top - $(window).scrollTop())>=0){
            $('#detail-holder').removeClass('fly');
            $('#detail-holder').width("286px");
        }
    }


});

$(document).ready(function(){
    if($('.book-form-passengers').length){
        $('.book-form-passengers').uniform();
    }
    

});



$('#adultNo').change(function(){
    valid_total_pax('#adultNo', '#childNo', '#infantNo', 9);
});