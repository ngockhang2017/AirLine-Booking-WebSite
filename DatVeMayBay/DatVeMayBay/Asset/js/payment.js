$(document).ready(function(){
			$("#1").click(function(){
				$(".all_atm").slideDown	()
				$(".infom").slideUp();
					//alert('ok');
				$("#1").attr('checked',checked);
				// có thẻ tạo hàm sau khi show hoặc hide
				return false;
			});
			// if("#1").attr('checked',"");
			// {
			// 	$(".all_atm").slideUp();
			// 	return false;
			// }
			$("div.sub_payment>input[id!='1']").click(function()
			{
				$(".all_atm").slideUp();
				$(".infom").slideUp();
				if(this.id==6)
				{
					$(".infom").slideDown()
				}
				$(this).attr('checked',checked);
				// có thẻ tạo hàm sau khi show hoặc hide
				return false;
			});
			$(".atm").click(function(){
				// $(".atm").css({border:'unset'});
				// $(this).css({border:'1px solid rgb(0, 173, 239)',});
				$(".atm").removeClass('box');
				$(this).addClass('box');
					
					
					return false;
			});
			// $("#6").click(function(){
			// 	$(".info").slideDown()
			// 		//alert('ok');
			// 	$("#6").attr('checked',checked);
			// 	// có thẻ tạo hàm sau khi show hoặc hide
			// 	return false;
			// });
			// $("div.sub_payment input[id!='6']").click(function()
			// {
			// 	$(".info").slideUp();
			// 	$(this).attr('checked',checked);
			// 	// có thẻ tạo hàm sau khi show hoặc hide
			// 	return false;
			// });
			// });

			

		});
			function checkFullname(textbox)
			{
				var fullname=$.trim($("#fullname").val());
				if(fullname==''||fullname.length <7)
				{

					$("#fullname_error").text('Họ và tên phải có ít nhất 7 kí tự');
				}
				else
				{
					$("#fullname_error").text('');
				}
					
			};
			function checkPhone(textbox)
			{
				var phone=$.trim($("#phone").val());
				 var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
				if(phone !==''){
        if (vnf_regex.test(phone) == false) 
        {
            $("#phone_error").text('Số điện thoại của bạn không đúng định dạng!');
        }else{
            $("#phone_error").text('');
        }
    	}else{
        $("#phone_error").text('Bạn chưa điền số điện thoại!');
    	}
					
			};

			function checkAddress(textbox)
			{
				var address=$.trim($("#address").val());
				if(address==''||address.length <8)
				{

					$("#address_error").text('Địa chỉ không đúng định dạng');
				}
				else
				{
					$("#address_error").text('');
				}
					
			};