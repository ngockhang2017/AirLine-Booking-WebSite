$(document).ready(function(){	
	$(window).scroll(function() {		
		if ($(document).height()-($(window).scrollTop() + $(window).height())<480) {
			if($("#submitFlight").hasClass('fixed')){
				$("#submitFlight").removeClass('fixed');
			}						
		}else{
			if(! $("#submitFlight").hasClass('fixed')){
				$("#submitFlight").addClass('fixed');
			}
		}
	});
	$("#deptime-slider").slider({
		range: true,
		min: 0,
		max: 24,
		values: [0, 24],
		slide: function(event, ui) {
			$("#choosen-range").val(ui.values[ 0 ] + "h - " + ui.values[ 1 ] + "h");
			var tb = [$('.datTable0').length && $('.datTable0') || null, $('.datTable1').length && $('.datTable1') || null , $('.datTable').length && $('.datTable') || null ];
			for (i = 0; i < tb.length; i++) {
				if (!tb[i])
					continue;
				$(tb[i]).dataTable().api().draw();
			}
		}
	});
	$("input[name='sortRule']").click(function(e) {
		sort_asc($(this).data('column-index'));
	});
	$('#filter-airline-list').on('click', 'input[name="selectedAir"]', function() {
        filter_show();
    });
});


function sort_asc(index) {
	var tb = [$('.datTable0').length && $('.datTable0') || null, $('.datTable1').length && $('.datTable1') || null , $('.datTable').length && $('.datTable') || null ];
	for (i = 0; i < tb.length; i++) {
		if (!tb[i])
			continue;
		tb[i].dataTable().api().order(index, 'asc').draw();
	}
}
function  addAirlineToFilter(row) {
	var exist = false;
	var airLineLength = $('#filter-airline-list li input').length;
	for (jIndex = 0; jIndex < airLineLength; jIndex++) {
		if ($($('#filter-airline-list li input')[jIndex]).data('aircode') == row.aircode) {
			exist = true;
		}
	}
	if (exist === false) {
		var _logo_img = Array.isArray(row.logo_img) ? row.logo_img[0] : row.logo_img;
		var _airline = Array.isArray(row.airline) ? row.airline[0] : row.airline;
		var _newNode = '<li><input type="checkbox" name="selectedAir" data-aircode="' + row.aircode + '" id="slAir' + _logo_img + '"><label style="margin-top: 0px;" for="slAir' + _logo_img + '">&nbsp;<img src="'+base_url+'airline_logo/sm' + _logo_img + '.gif" title="' + _airline + '"><div style="line-height: 14px; font-weight: bold;">' + _airline + '</div></label></li>';
		$('#filter-airline-list').append(_newNode);
	}
}
function filter_show() {
    var tb = [$('.datTable0').length && $('.datTable0') || null, $('.datTable1').length && $('.datTable1') || null, $('.datTable').length && $('.datTable') || null];
    for (i = 0; i < tb.length; i++) {
        if (!tb[i])
            continue;
        buildRegxFilter(tb[i], 'input[name=\'selectedAir\']', 0);
    }

}
function buildRegxFilter(target, ruleContainer, onColumn)
{
    var regx = '';
    $(ruleContainer).each(function() {
        if ($(this).is(":checked")) {
            var _aircode = $(this).data('aircode');
            regx += _aircode + '|';
        }
    });
    regx = regx.slice(0, -1);
    this.applyFilter(target, onColumn, regx);
}

function applyFilter(target, column, regx)
{
    $(target).dataTable().api().column(column).search(regx, true, null).draw();
}
	

	
	
function add_to_sidebar(group, way, index){
	if(data.engine==4 && tickets[group][way][index].depcity==null){
		return false;
	}
	if(! $('#detail-holder').find('#chieudi').length){
		var header = document.createElement("div");
		var chieudi =  document.createElement("div");
		var chieuve =  document.createElement("div");
		var subtotal =  document.createElement("div");
		header.innerHTML = '';
		chieudi.id = 'chieudi';
		chieuve.id = 'chieuve';
		subtotal.id = 'subtotal';
		chieudi.className= chieuve.className = 'way-holder hidden';
		subtotal.className="hidden";
		subtotal.innerHTML = 'Tổng cộng: <span id="total-price"></span>';
		$('#detail-holder').append(header, chieudi, chieuve, subtotal);
	}
	var caption = null, sel = null;
	
	if(parseInt(way)>0){
		caption = 'Giá vé lượt về';
		sel = '#chieuve';
	}else{
		caption = 'Giá vé lượt đi';
		sel = '#chieudi';
	}
	
	var _tic = tickets[group][way][index];
	
	//html = '<h4 class="way"><img src="airline_logo/sm'+_tic.airlinecode+'.gif" />'+caption+' - '+_tic.flightno+'<span class="fa fa-sort-desc pull-right" toggle="detail"></span></h4>'
	html = '<h4 class="way">'+caption+' - '+_tic.flightno+': '+'<span class="fax fa-sort-descx pull-right" toggle="detail">'+accounting.formatMoney(_tic.subtotal)+'</span></h4>';
	html += '<div aria="detail" style=" ">';
	html += '<ul>';
	html += '<li><strong>Hành trình: </strong>'+_tic.depcity +' - ' + _tic.descity+'</li>';
	html += '<li><strong>Khởi hành: </strong>'+_tic.deptime+' '+_tic.depdate+'</li>';
	html += '<li><strong>Đến: </strong>'+_tic.arvtime+' '+_tic.arvdate+'</li>';	
	html += '</ul>'; 
	html += '<p style="display: none;"><strong>Tổng: </strong>'+accounting.formatMoney(_tic.subtotal)+'</p>';
	html += '</div>'; 
	$(sel).html(html);
	
	var _total = 0;
	$('.way-holder p').each(function(){
		
		var value = parseInt($(this).text().replace(/\./g,'').match(/\d+/).toString());
		_total+=value;
	});	
	
	
	$('#total-price').text(accounting.formatMoney(_total));
	if($(sel).hasClass('hidden')){
		$(sel).removeClass('hidden');
	}
	if($('#subtotal').hasClass('hidden')){
		$('#subtotal').removeClass('hidden');
	}
}
	
	function add_to_sidebar_old(group, way, index){
	if(data.engine==4 && tickets[group][way][index].depcity==null){
		return false;
	}
	if(! $('#detail-holder').find('#chieudi').length){
		var header = document.createElement("div");
		var chieudi =  document.createElement("div");
		var chieuve =  document.createElement("div");
		var subtotal =  document.createElement("div");
		header.innerHTML = '<h1 class="title-info">Thông tin chuyến bay</h1>';
		chieudi.id = 'chieudi';
		chieuve.id = 'chieuve';
		subtotal.id = 'subtotal';
		chieudi.className= chieuve.className = 'way-holder hidden';
		subtotal.className="hidden";
		subtotal.innerHTML = 'Tổng cộng: <span id="total-price"></span>';
		$('#detail-holder').append(header, chieudi, chieuve, subtotal);
	}
	var caption = null, sel = null;
	
	if(parseInt(way)>0){
		caption = 'Chiều về';
		sel = '#chieuve';
	}else{
		caption = 'Chiều đi';
		sel = '#chieudi';
	}
	
	var _tic = tickets[group][way][index];
	
	//html = '<h4 class="way"><img src="airline_logo/sm'+_tic.airlinecode+'.gif" />'+caption+' - '+_tic.flightno+'<span class="fa fa-sort-desc pull-right" toggle="detail"></span></h4>'
	html = '<h4 class="way">'+caption+' - '+_tic.flightno+'<span class="fa fa-sort-desc pull-right" toggle="detail"></span></h4>'
	html += '<div aria="detail">';
	html += '<ul>';
	html += '<li><strong>Hành trình: </strong>'+_tic.depcity +' - ' + _tic.descity+'</li>';
	html += '<li><strong>Khởi hành: </strong>'+_tic.deptime+' '+_tic.depdate+'</li>';
	html += '<li><strong>Đến: </strong>'+_tic.arvtime+' '+_tic.arvdate+'</li>';	
	html += '</ul>';
    if(_tic.stop.length){
        html += '<h5>Chi tiết hành trình</h5>'
        for(var i = 0 ; i< _tic.stop.length; i++){
           _stop = _tic.stop[i] ;
            html += "<table class='tbl-flight-detail'>"
           if(_stop.depairport!=_stop.desairport){
              
              html += "<tr><td>"+_stop.depcity+"</td><td>"+_stop.descity+"</td><td>"+_stop.flightno+"</td></tr>" 
              html += "<tr><td>"+_stop.depdate+"</td><td>"+_stop.desdate+"</td><td></td><tr>" 
              html += "<tr><td>"+_stop.deptime+"</td><td>"+_stop.destime+"</td><td></td></tr>" 
              
           }else{
               html+= "<tr><td colspan='3' class='stop'>Chờ tại "+_stop.depcity+" - "+_stop.waittime+"</td></tr>"
           }
          
                  
           html += "</table>"
        }
    }
    
	html += '<h5>Giá vé</h5>';
	html += '<ul class="detail">';
	html += '<li><b>Người lớn:</b></li>';
	html += '<li>'+_tic.adult.num+' x ('+accounting.formatMoney(_tic.baseprice)+' + '+accounting.formatMoney(_tic.adult.taxfee)+') = '+accounting.formatMoney(_tic.adult.total)+'</li>';
	if(_tic.child.num>0){
		html += '<li><b>Trẻ em:</b></li>';
		html += '<li>'+_tic.child.num+' x ('+accounting.formatMoney(_tic.child.baseprice)+' + '+accounting.formatMoney(_tic.child.taxfee)+') = '+accounting.formatMoney(_tic.child.total)+'</li>';
	}
	if(_tic.infant.num>0){
		html += '<li><b>Em bé:</b></li>';
		html += '<li>'+_tic.infant.num+' x ('+accounting.formatMoney(_tic.infant.baseprice)+' + '+accounting.formatMoney(_tic.infant.taxfee)+') = '+accounting.formatMoney(_tic.infant.total)+'</li>';
	}
	html += '</ul>';
	html += '<p><strong>Tổng: </strong>'+accounting.formatMoney(_tic.subtotal)+'</p>';
	html += '</div>';
	
	$(sel).html(html);
	
	var _total = 0;
	$('.way-holder p').each(function(){
		
		var value = parseInt($(this).text().replace(/\./g,'').match(/\d+/).toString());
		_total+=value;
	});	
	
	
	$('#total-price').text(accounting.formatMoney(_total));
	if($(sel).hasClass('hidden')){
		$(sel).removeClass('hidden');
	}
	if($('#subtotal').hasClass('hidden')){
		$('#subtotal').removeClass('hidden');
	}
}

function strtotime(str){
    var arrTime = str.split('/');
    var jsDay = 60 * 60 * 24 * 1000;
    var workTime = new Date(arrTime[2], arrTime[1] - 1, arrTime[0]);
    var workTimestamp = workTime.getTime() - 3 * jsDay;
    return workTimestamp;
}