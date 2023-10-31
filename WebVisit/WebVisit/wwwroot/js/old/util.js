/**
* 유틸리티 함수 모음
* 1.common
* 2.cookie
* 3.date
* 4.number
* 5.openwin
* 6.text
* 7.validate
*/

//1.common


window.document.onclick = function() {

	eventElement = window.event.srcElement;

	if (eventElement.tagName == "INPUT" )
	{
		// type button 점선 안생기도록
		if ( eventElement.type.toUpperCase() == "BUTTON" )
		{
			eventElement.blur();
		}
		// readonly 상태인 경우 backspace 로 뒤로가기 막기
		else if (eventElement.readOnly == true )
		{
			eventElement.onkeydown = function ()
			{
				if (event.keyCode == 8 || event.keyCode == 116) {
					event.keyCode==0;
					event.cancelBubble = true;
					event.returnValue = false;
				}
			}
		}
	}
}



//maxlength 만큼 옮기면 다음으로 이동하기....
function nextFocus(sFormName,sNow,sNext)
{
	var sForm = 'document.'+ sFormName +'.'
	var oNow = eval(sForm + sNow);

	if (typeof oNow == 'object')
	{
		if ( oNow.value.length == oNow.maxLength)
		{
			var oNext = eval(sForm + sNext);

			if ((typeof oNext) == 'object')
				oNext.focus();
		}
	}
}
//숫자만 입력
function makeNumberOnly() {
	if(event.keyCode < 48 || event.keyCode > 57) {
	event.returnValue = false;
	}
}





//2.cookie

/**
 * Cookie값 가져오기
 */

var getCookie = function(name){
	var nameOfCookie = name + "=";
	var x = 0;
	while ( x <= document.cookie.length ){
		var y = (x+nameOfCookie.length);
		if ( document.cookie.substring( x, y ) == nameOfCookie ) {
			if ( (endOfCookie=document.cookie.indexOf( ";", y )) == -1 )
				endOfCookie = document.cookie.length;
			return unescape( document.cookie.substring( y, endOfCookie ) );
		}
		x = document.cookie.indexOf( " ", x ) + 1;
		if ( x == 0 )
			break;
	}
	return "";
}

/**
 * Cookie값 저장하기
 */
var setCookie = function(name, value) {
	var argv = setCookie.arguments;
	var argc = setCookie.arguments.length;
	var expires = (2 < argc) ? argv[2] : null;
	var toDate = new Date();
		toDate.setDate( toDate.getDate() + expires );
	var path = (3 < argc) ? argv[3] : "/";
	var domain = (4 < argc) ? argv[4] : null;
	var secure = (5 < argc) ? argv[5] : false;
	document.cookie = name + "=" + escape (value)
			+ ((expires == null) ? "" : ("; expires=" + toDate.toGMTString()))
			+ ((path == null) ? "" : ("; path=" + path))
			+ ((domain == null) ? "" : ("; domain=" + domain))
			+ ((secure == true) ? "; secure" : "");
}

//3.date
/**
 * 날짜형식 표시
 */
var formateDate = function(val){
	var result = cfNumeric(val);

	if (result.length == 8 )
	{
		result	= result.substr(0,4)+"/"+result.substr(4,2)+"/"+result.substr(6,2);
	}
	else if (result.length == 14 )
	{
		result	= result.substr(0,4)+"/"+result.substr(4,2)+"/"+result.substr(6,2)+" "
			+ result.substr(8,2)+":"+result.substr(10,2)+":"+result.substr(12,2);
	}
	else
	{
		result = val;
	}

	return result ;
}

/**
 *
 */
var cfNumeric = function(sOrg){
	var nm = "0";

	if (sOrg != null)
	{
		sOrg = sOrg.replace(/[^0-9]/g,"");
		nm = sOrg;
		//nm = parseFloat(sOrg).toString();
	}

	return (isNaN(nm)?"":nm);
}


//4.number

/**
 * <pre>
 * 숫자나 문자열을 통화(Money) 형식으로 만든다.
 * &lt;input type="text" name="test" value="" onkeyup="this.value=toCurrency(this.value);"&gt;
 * or
 * var num = toCurrency(document.form[0].text.value);
 * </pre>
 * @param	amount	"1234567"
 * @return	currencyString "1,234,567"
 */
var toCurrency = function(amount){
	amount = String(amount);

	var data = amount.split('.');

	if (data[0] == "") data[0]="0";

/*
	if (data[1] != null && data[1].length > 2) {
		alert("소수점 2자리까지만 입력 가능합니다.");
		return data[0] + "." + data[1].substring(1,3);
	}
*/
	var sign = "";

	var firstChar = data[0].substr(0,1);
	if(firstChar == "-"){
		sign = firstChar;
		data[0] = data[0].substring(1, data[0].length);
	}

	data[0] = data[0].replace(/\D/g,"");
	if(data.length > 1){
		data[1] = data[1].replace(/\D/g,"");
	}

	firstChar = data[0].substr(0,1);

	//0으로 시작하는 숫자들 처리
	if(firstChar == "0"){
		if(data.length == 1){
			return sign + parseFloat(data[0]);
		}
	}

	var comma = new RegExp('([0-9])([0-9][0-9][0-9][,.])');

	data[0] += '.';
	do {
		data[0] = data[0].replace(comma, '$1,$2');
	} while (comma.test(data[0]));

	if (data.length > 1) {
		return sign + data.join('');
	} else {
		return sign + data[0].split('.')[0];
	}
}

/**
 * <pre>
 * 숫자나 문자열을 통화(Money) 형식으로 만든다.
 * 단, 양수만 허용한다.
 * &lt;input type="text" name="test" value="" onkeyup="this.value=toCurrency(this.value);"&gt;
 * or
 * var num = toCurrency(document.form[0].text.value);
 * </pre>
 * @param	amount	"1234567"
 * @return	currencyString "1,234,567"
 * @see #toCurrency(amount)
 */
var toCurrencyPositive = function(amount){
	var firstChar = amount.substr(0,1);
	if(firstChar == "-"){
		amount = amount.substring(1, amount.length);
	}
	return toCurrency(amount);
}


/**
 * <pre>
 * 숫자나 문자열을 숫자만 표시
 * &lt;input type="text" name="test" value="" onkeyup="this.value=toNumPoint(this.value);"&gt;
 * or
 * var num = toNumPoint(document.form[0].text.value);
 * </pre>
 * @param	amount	"1234567"
 * @return	currencyString "1234567"
 */
var toNumPoint = function(amount){
	amount = String(amount);

	var data = amount.split('.');

	if (data[0] == "") data[0]="0";
/*
	if (data[1] != null && data[1].length > 2) {
		alert("소수점 2자리까지만 입력 가능합니다.");
		return data[0] + "." + data[1].substring(1,3);
	}
*/
	var sign = "";

	var firstChar = data[0].substr(0,1);
	if(firstChar == "-"){
		sign = firstChar;
		data[0] = data[0].substring(1, data[0].length);
	}

	data[0] = data[0].replace(/\D/g,"");
	if(data.length > 1){
		data[1] = data[1].replace(/\D/g,"");
	}

	firstChar = data[0].substr(0,1);

	//0으로 시작하는 숫자들 처리
	if(firstChar == "0"){
		if(data.length == 1){
			return sign + parseFloat(data[0]);
		}
	}

	var comma = new RegExp('[^0-9.]');

	data[0] += '.';

	data[0] = data[0].replace(comma, '');

	if (data.length > 1) {
		return sign + data.join('');
	} else {
		return sign + data[0].split('.')[0];
	}
}

//5.openwin
/**
* {url:"", winname:"", title:"", type, params:{}, autoresize:false}
* type : s->small(350*400), m->middle(550*400), l->large(750*400)
* 		  type에 따라 width는 고정. height는 변동가능
*/
var popup = function(pin) {
	var defaultProps = {
		winname:"",
		type:"s",
		title:"",
		params:{},
		height:"",
		width:"",
		left:"",
		top:"",
		scrollbars:false,
		resizable:true
	};

	if ( !pin.width) {
		switch (pin.type) {
			case "s":
				pin.width = "375";
				break;
			case "m":
				pin.width = "575";
				break;
			case "l":
				pin.width = "775";
				break;
		}
	}
	if ( !pin.height ) {
		pin.height = "400";
	}

	pin = $.extend(defaultProps, pin||{});

	var params = "";
	$.each(pin.params, function(name, value){
		params += ("&" + name + "=" + encodeURI(value));
	});

	var _left = (screen.width)/2 - pin.width/2 ;
	var _top = (screen.height)/2 - pin.height/2;
	if ( pin.top) {
		_top = pin.top;
	}
	if( pin.left) {
		_left = pin.left;
	}

	var win = window.open(
				pin.url + "?title=" + encodeURI(pin.title) + "&type=" + pin.type + params,
				pin.winname,
				"menubar=no, scrollbars=" + (pin.scrollbars ? "yes" : "no") + ", resizable=" + (pin.resizable ? "yes" : "no") + ", status=yes, width=" + pin.width + ", height=" + pin.height+ ",top=" + _top + ",left=" + _left + "");
	if(win != undefined && win != null ){
		win.focus();
	}
	return win;
};

//6.text
/**
* 앞뒤 빈공간 삭제
* @param str stirng
* @return string
*/
String.prototype.trim = function() {
	return this.replace(/^\s+/g,'').replace(/\s+$/g,'');
};

var trim = function(str) {
	if(typeof(str) == "undefined") return "";
	return str.replace(/(^\s*)|(\s*$)/gi, "");
}

String.prototype.replaceAll = function(from, to) {
  return this.replace(new RegExp(from, "g"), to);
}

/**
* 글자수 체크
* ex) onFocus=getLenth(this,50) onKeyUp=getLenth(this,50)
*/
var getLenth = function(field, maxlimit) {
	var charcnt = field.value.length;
	var temp ="";
	var enter_cnt = 0;
	for(i = 0 ; i < charcnt ; i++){
		temp = field.value.charAt(i);
		if(temp == '\n'){
			enter_cnt++; // 엔터키
		}
	}
	if((charcnt - enter_cnt*2) <= maxlimit){
		return false;
	} else {
		alert("최대 " + maxlimit + "자 이내로 입력해주세요");
		//field.value = field.value.substring(0, (maxlimit+(enter_cnt*2)));
		return true;
	}
};

/**
* byte수 조회
* obj : 대상 문자열 또는 필드
*/
var getByteLength = function(obj){
	var s;
	if (typeof(obj) == "string") {	//문자열
		s = obj;
	} else {
		s = obj.value;
	}
	var len = 0;
	if ( s == null ) {
		return 0;
	}
	for(var i = 0 ; i < s.length ; i ++) {
		var c = escape(s.charAt(i));
		if ( c.length == 1 ) {
			len ++;
		} else if ( c.indexOf("%u") != -1 ) {
			len += 2;
		} else if ( c.indexOf("%") != -1 ) {
			len += c.length/3;
		}
	}
	return len;
};

var checkByteLength = function(obj, maxlimit){
	var s;
	var len = 0;

	if (typeof(obj) == "string") {	//문자열
		s = obj;
	} else {
		s = obj.value;
	}

	if ( s != null )
	{
		for(var i = 0 ; i < s.length ; i ++) {
			var c = escape(s.charAt(i));
			if ( c.length == 1 ) {
				len ++;
			} else if ( c.indexOf("%u") != -1 ) {
				len += 2;
			} else if ( c.indexOf("%") != -1 ) {
				len += c.length/3;
			}

			if (len > maxlimit ) {
				alert("최대 " + maxlimit + "byte 이내로 입력해주세요");
				obj.value = s.substring(0, i);
				return true;
			}
		}
	}

	return false;
}

/**
* 10보다 작으면 앞에 0을 붙임
*/
var addzero = function(n) {
	return n < 10 ? "0" + n : n;
};

//7.validate
/**
* <pre>
* NumberCheck
* 숫자인지 여부체크.
* </pre>
* @param field form.element
* @param error_msg 에러 message
* @return boolean
*/
var isNumberVal = function(val){

	if(checkDigitOnly(val, false) ) {
		return true;
	} else {
		return false;
	}
};

/**
* <pre>
* 숫자인지 아닌지  검사한다.
* 검사할 값이 "" 일 경우 true를 리턴하고 싶으면, space인수에 true를 넣으면 된다.
* </pre>
* @param digitChar 검사할 string
* @param space ""일 때 허용여부(true||false)
* @return boolean
*/
var checkDigitOnly = function(digitChar, space) {
	if(!space){
		if ( digitChar == null || digitChar=='' ){
		return false ;
	}
	}

	for(var i=0;i<digitChar.length;i++){
	var c=digitChar.charCodeAt(i);
	if( !(  0x30 <= c && c <= 0x39 ) ) {
		return false ;
	}
   }

  return true ;
};

/**
* <pre>
* isNumber
* 빈공간을 허용한다.
* </pre>
* @param field form.element
* @param error_msg 에러 message
* @return boolean
*/
var isNumber = function(field, error_msg){
	var val = field.value;

	if(checkDigitOnly(val, false) ) {
		return true;
	} else {
		if(error_msg.length > 0) {
			alert(error_msg);
			field.focus();
			field.select();
		}
		return false;
	}
};

// ExtJS 용
var isNumber_Ext = function(field, error_msg){
	var val = field.value;

	if(checkDigitOnly(val, false) ) {
		return true;
	} else {
		if(error_msg.length > 0) {
			Ext.MessageBox.alert("Status", error_msg,function(btn,text){
				if(btn=="ok"){
					field.focus();
					field.select();
				}
			});

		}
		return false;
	}
};


/**
* 날짜 형식 검사
*/
var checkDate = function(inja) {
	var result = false;


	if (inja.match(/[0-9]{4}\/[0-9]{2}\/[0-9]{2}/) == null )
	{
		result = true;
	}
	else if ( inja.length != '10')
	{
		result = true;
	}
	else
	{
		var vDate = removeChar(inja);
		var yyyy  = vDate.substr(0,4);
		var mm    = vDate.substr(4,2);
		var dd    = vDate.substr(6,2);

		var date = new Date(yyyy +"/"+ mm +"/"+ dd);

		if (yyyy - date.getFullYear() != 0) {
			alert("날짜 형식이 올바르지 않습니다.");
			result = true;
		}
		else if (mm - date.getMonth() - 1 != 0) {
			alert("날짜 형식이 올바르지 않습니다.");
			result = true;
		}
		else if (dd - date.getDate() != 0) {
			alert("날짜 형식이 올바르지 않습니다.");
			result = true;
		}
	}



	return result ;
};

/**
* <pre>
* 문자열 Valid 검사처리
* </pre>
* @param value
* @param space
* @return boolean
*/
var checkValid = function(value, space) {
	var retvalue = false;

	//value이 0("" 이나 null)이면 무조건 false
	for (var i = 0 ; i < value.length ; i++) {
		if (space == true) {
			//value이 0이 아닐때 space가 있어야만 true(valid)
			if (value.charAt(i) == ' ') {
				retvalue = true;
				break;
			}
		} else {
			//value이 0이 아닐때 space가 아닌 글자가 있어야만 true(valid)
			if (value.charAt(i) != ' ') {
				retvalue = true;
				break;
			}
		}
	}

	return retvalue;
};

/**
* <pre>
* field Empty 및 공백 처리
* error_msg가 ""이면 alert와 focusing을 하지 않는다
* </pre>
* @param field form.element
* @param error_msg 에러 Message
* @return boolean
*/
var isEmpty = function(field, error_msg) {

	// error_msg가 ""이면 alert와 focusing을 하지 않는다
	if(error_msg == "") {
		if(!checkValid(field.value, false)) {
			return true;
		} else {
			return false;
		}
	} else {
		if(!checkValid(field.value, false)) {
			alert(error_msg);
			field.focus() ;
			return true;
		} else {
			return false;
		}
	}
};

// ExtJS용
var isEmpty_Ext = function(field, error_msg) {

	// error_msg가 ""이면 alert와 focusing을 하지 않는다
	if(error_msg == "") {
		if(!checkValid(field.value, false)) {
			return true;
		} else {
			return false;
		}
	} else {
		if(!checkValid(field.value, false)) {
			Ext.MessageBox.alert("Status", error_msg,function(btn,text){
				if(btn=="ok") field.focus();
			});
			return true;
		} else {
			return false;
		}
	}
};


/**
* 주민등록번호 Check
* @param pid1 주민번호 앞자리 - form.element
* @param pid2 주민번호 뒤자리 - form.element
* @return boolean
*/
var isNotValidPID = function(pid1, pid2) {
	if(isEmpty(pid1,"주민등록번호를 입력해 주세요!")) {
		return true;
	}
	if(isEmpty(pid2,"주민등록번호를 입력해 주세요!")) {
		return true;
	}
	if(!isNumber(pid1,"주민등록번호 앞자리는 숫자로만 기입해 주세요!")) {
		return true;
	}
	if(!isNumber(pid2,"주민등록번호 뒷자리는 숫자로만 기입해 주세요!")) {
		return true;
	}
	if(isNotExactLength(pid1, 6, "주민등록번호 앞자리는 6자리입니다!")) {
		return true;
	}
	if(isNotExactLength(pid2, 7, "주민등록번호 뒷자리는 7자리입니다!")) {
		return true;
	}
	strchr = pid1.value.concat(pid2.value);
	if (strchr.length == 13	) {
		nlength = strchr.length;
		num1 = strchr.charAt(0);
		num2 = strchr.charAt(1);
		num3 = strchr.charAt(2);
		num4 = strchr.charAt(3);
		num5= strchr.charAt(4);
		num6 = strchr.charAt(5);
		num7 = strchr.charAt(6);
		num8 = strchr.charAt(7);
		num9 = strchr.charAt(8);
		num10 = strchr.charAt(9);
		num11 = strchr.charAt(10);
		num12 = strchr.charAt(11);

		var total = (num1*2)+(num2*3)+(num3*4)+(num4*5)+(num5*6)+(num6*7)+(num7*8)+(num8*9)+(num9*2)+(num10*3)+(num11*4)+(num12*5);
		total = (11-(total%11)) % 10;

		if(total != strchr.charAt(12)) {
			alert("주민등록번호가 올바르지 않습니다. \n다시 입력해 주세요!");
			pid1.value="";
			pid2.value="";
			pid1.focus();
			return true;
		}
		return false;
	} else {
		alert("주민등록번호가 올바르지 않습니다. \n다시 입력해 주세요!");
		pid1.value = "";
		pid2.value = "";
		pid1.focus();
		return true;
	}
};

/**
* 주민등록번호 Check (ExtJS 용)
* @param pid1 주민번호 앞자리 - form.element
* @param pid2 주민번호 뒤자리 - form.element
* @return boolean
*/
var isNotValidPID_Ext = function(pid1, pid2) {

	if(isEmpty_Ext(pid1,"주민등록번호를 입력해 주세요!")) {
		return true;
	}
	if(isEmpty_Ext(pid2,"주민등록번호를 입력해 주세요!")) {
		return true;
	}
	if(!isNumber_Ext(pid1,"주민등록번호 앞자리는 숫자로만 기입해 주세요!")) {
		return true;
	}
	if(!isNumber_Ext(pid2,"주민등록번호 뒷자리는 숫자로만 기입해 주세요!")) {
		return true;
	}
	if(isNotExactLength_Ext(pid1, 6, "주민등록번호 앞자리는 6자리입니다!")) {
		return true;
	}
	if(isNotExactLength_Ext(pid2, 7, "주민등록번호 뒷자리는 7자리입니다!")) {
		return true;
	}
	strchr = pid1.value.concat(pid2.value);
	if (strchr.length == 13	) {
		nlength = strchr.length;
		num1 = strchr.charAt(0);
		num2 = strchr.charAt(1);
		num3 = strchr.charAt(2);
		num4 = strchr.charAt(3);
		num5= strchr.charAt(4);
		num6 = strchr.charAt(5);
		num7 = strchr.charAt(6);
		num8 = strchr.charAt(7);
		num9 = strchr.charAt(8);
		num10 = strchr.charAt(9);
		num11 = strchr.charAt(10);
		num12 = strchr.charAt(11);

		var total = (num1*2)+(num2*3)+(num3*4)+(num4*5)+(num5*6)+(num6*7)+(num7*8)+(num8*9)+(num9*2)+(num10*3)+(num11*4)+(num12*5);
		total = (11-(total%11)) % 10;

		if(total != strchr.charAt(12)) {
			Ext.MessageBox.alert("Status", "주민등록번호가 올바르지 않습니다. \n다시 입력해 주세요!",function(btn,text){
				if(btn=="ok"){
					pid1.value="";
					pid2.value="";
					pid1.focus();
				}
			});
			return true;
		}
		return false;
	} else {
		Ext.MessageBox.alert("Status", "주민등록번호가 올바르지 않습니다. \n다시 입력해 주세요!",function(btn,text){
			if(btn=="ok"){
				pid1.value = "";
				pid2.value = "";
				pid1.focus();
			}
		});
		return true;
	}
};



/**
* <pre>
* 정확한 길이가  아닌지 검사
* 정확한 길이면 false, 정확한 길이가 아닌면 true
* </pre>
* @param field 길이를 검사할 element form.element
* @param len 비교할 길이
* @param error_msg 에러 Message
* @return boolean
*/
var isNotExactLength = function(field, len, error_msg) {
	if(field.value.length != len) {
		alert(error_msg);
		field.focus();
		field.select();
		return true;
	}
	return false;
}

// ExtJS 용
var isNotExactLength_Ext = function(field, len, error_msg) {
	if(field.value.length != len) {
		Ext.MessageBox.alert("Status", error_msg,function(btn,text){
			if(btn=="ok"){
				field.focus();
				field.select();
			}
		});
		return true;
	}
	return false;
}

/**
* 사업자등록번호 Check
* @param bid1 사업자등록번호 앞자리 - form.element
* @param bid2 사업자등록번호 중간 자리 - form.element
* @param bid3 사업자등록번호 중간 자리 - form.element
* @return boolean
*/
var isNotValidBID = function(bid1, bid2, bid3) {

	if(isEmpty(bid1,"사업자등록번호를 입력해 주세요!")) {
		return true;
	}
	if(isEmpty(bid2,"사업자등록번호를 입력해 주세요!")) {
		return true;
	}
	if(isEmpty(bid3,"사업자등록번호를 입력해 주세요!")) {
		return true;
	}
	if(!isNumber(bid1,"사업자등록번호 앞자리는 숫자로만 기입해 주세요!")) {
		return true;
	}
	if(!isNumber(bid2,"사업자등록번호 가운데자리는 숫자로만 기입해 주세요!")) {
		return true;
	}
	if(!isNumber(bid3,"사업자등록번호 뒷자리는 숫자로만 기입해 주세요!")) {
		return true;
	}
	if(isNotExactLength(bid1, 3, "사업자등록번호 앞자리는 3자리입니다!")) {
		return true;
	}
	if(isNotExactLength(bid2, 2, "사업자등록번호 뒷자리는 2자리입니다!")) {
		return true;
	}
	if(isNotExactLength(bid3, 5, "사업자등록번호 뒷자리는 5자리입니다!")) {
		return true;
	}
	strchr = bid1.value.concat(bid2.value.concat(bid3.value));

	num1 = strchr.charAt(0);
	num2 = strchr.charAt(1);
	num3 = strchr.charAt(2);
	num4 = strchr.charAt(3);
	num5= strchr.charAt(4);
	num6 = strchr.charAt(5);
	num7 = strchr.charAt(6);
	num8 = strchr.charAt(7);
	num9 = strchr.charAt(8);
	num10 = strchr.charAt(9);

	var total = (num1*1)+(num2*3)+(num3*7)+(num4*1)+(num5*3)+(num6*7)+(num7*1)+(num8*3)+(num9*5);
	total = total + parseInt((num9 * 5) / 10);
	var tmp = total % 10;
	if(tmp == 0) {
		var num_chk = 0;
	} else {
		var num_chk = 10 - tmp;
	}

	if(num_chk != num10) {
		alert("사업자등록번호가 올바르지 않습니다. \n다시 입력해 주세요!");
		bid1.value="";
		bid2.value="";
		bid3.value="";
		bid1.focus();
		return true;
	}
	return false;
};

/**
*  E-Mail Check
* @param field form.element
* 		  dispFld form.element 오류발생시 커서이동시킬 element
* @return booelan
*/
var isNotValidEmail =  function(field, dispFld) {
	var checkflag = true;
	var retvalue;

	if(field.value == "") {
		retvalue = true;
	} else {
		if (window.RegExp) {
			var tempstring = "a";
			var exam = new RegExp(tempstring);
			if (tempstring.match(exam)) {
				var ret1 = new RegExp("(@.*@)|(\\.\\.)|(@\\.)|(^\\.)");
				var ret2 = new RegExp("^.+\\@(\\[?)[a-zA-Z0-9\\-\\.]+\\.([a-zA-Z]{2,3}|[0-9]{1,3})(\\]?)$");
				retvalue = (!ret1.test(field.value) && ret2.test(field.value));
			} else {
				checkflag = false;
			}
		} else {
			checkflag = false;
		}
		if (!checkflag) {
			retvalue = ( (field.value != "") && (field.value.indexOf("@")) > 0 && (field.value.index.Of(".") > 0) );
		}
	}
	if(retvalue) {
		return false;
	} else {
		alert("이메일 주소가 정확하지 않습니다. \n다시 입력해 주세요!");
		try {
			field.focus();
			field.select();
		} catch(e) {
			if(typeof(dispFld) != "undefined") {
				dispFld.focus();
				dispFld.select();
			}
		}
		return true;
	}
};

/**
* TelNumber Check
* @param field form.element
* @return boolean
*/
var isNotValidTel = function(field) {
	var count;
	var permitChar = "0123456789-";
	for (var i = 0; i < field.value.length; i++) {
		count = 0;
		for (var j = 0; j < permitChar.length; j++) {
			if(field.value.charAt(i) == permitChar.charAt(j)) {
				count++;
				break;
			}
		}
		if (count == 0) {
			alert("전화번호가 정확하지 않습니다. \n다시 입력해 주세요!")
			field.focus();
			field.select();
			return true;
			break;
		}
	}
	return false;
};

/**
* 전화번호 검사(국번:selectbox, 번호1:text, 번호2:text 일 경우 사)
*/
var checkTelno = function(tel1, tel2, tel3) {
	if ( tel1.value == "" ) {
		alert("국번을 선택하세요!");
		tel1.focus();
		return true;
	}
	if ( tel2.value.trim() == "" || tel2.value.trim().length < 3 ) {
		alert("전화번호를 3자리이상 숫자로 입력하세요");
		tel2.focus();
		return true;
	}
	if (isNaN(tel2.value)) {
		alert("전화번호를 3자리이상 숫자로 입력하세요");
		tel2.focus();
		return true;
	}
	tel2.value = tel2.value.trim();
	if ( tel3.value.trim() == "" || tel3.value.trim().length < 4 ) {
		alert("전화번호를 4자리이상 숫자로 입력하세요");
		tel3.focus();
		return true;
	}
	if (isNaN(tel3.value)) {
		alert("전화번호를 4자리이상 숫자로 입력하세요");
		tel3.focus();
		return true;
	}
};

/**
* layer 보기 / 숨기기
*/
var showLayer = function(layer, over) {
	var div = document.getElementById(layer);
	if(div != null) {
		if(over == 'show') {
			div.style.display='';
		} else {
			div.style.display='none';
		}
	}
}


//레이어닫기
var closeCommonLayer = function(layer_name){
	document.getElementById(layer_name).innerHTML = "";
	show_ly(layer_name,'none');
}

/**
* 패스워드 유효성 검사
* 1.6자 이상 ~ 15자 이하
* 2.영대문자, 영소문자, 숫자, 특수기호중 2가지 이상이ㅡ 조합
* 3.동일문자 3회이상 반복 불가
* 4.키보드상 연속문자열 4자 이상 사용불가
* 5.사용자ID와 연속 3문자 이상 중복 불가
* 6.연속된 숫자/문자 3자 이상 사용불가
*
* 패스워드가 부적합하면 true 리턴
*/
var checkPassword = function(passwd, usr_id) {

	//숫자/문자의 순서대로 3자 이상 사용금지
	var strights = ['012345678901', '987654321098', 'abcdefghijklmnopqrstuvwxyzab', 'zyxwvutsrqponmlkjihgfedcbazy'];

	//연속된 키보드 조합
	var keypads = [
				'`1234567890-=', 	'=-0987654321`', 	'~!@#$%^&*()_+', 	'+_)(*&^%$#@!~',
				'qwertyuiop[]\\', 	'\\][poiuytrewq', 	'QWERTYUIOP{}|',	'|}{POIUYTREWQ',
				'asdfghjkl;\'', 	'\';lkjhgfdsa', 	'ASDFGHJKL:"', 		'":LKJHGFDSA',
				'zxcvbnm,./', 		'/.,mnbvcxz', 		'ZXCVBNM<>?', 		'?><MNBVCXZ'
				];

	var getPattern = function(str, casesensitive) {

		//정규식 생성전에 예약어를 escape 시킨다.
		var reserves = ['\\', '^', '$', '.', '[', ']', '{', '}', '*', '+', '?', '(', ')', '|'];

		$.each(reserves, function(index, reserve){
			var pattern = new RegExp('\\' + reserve, 'g');
			if (pattern.test(str)) {
				str = str.replace(pattern, '\\' + reserve);
			}
		});
		var pattern = null;
		if (casesensitive == false) {
			pattern = new RegExp(str, 'i');
		} else {
			pattern = new RegExp(str);
		}

		return pattern;
	}

	if (passwd.match(/^.{6,15}$/g) == null) {
		alert('패스워드는 6자리 이상 15자리 미만으로 입력하세요.');
		return true;
	}

	var valid_count = 0;
	if (passwd.match(/[a-z]/) != null) {
		valid_count++;
	}
	if (passwd.match(/[A-Z]/) != null) {
		valid_count++;
	}
	if (passwd.match(/[0-9]/) != null) {
		valid_count++;
	}
	if (passwd.match(/\W/) != null) {
		valid_count++;
	}

	if(valid_count < 2) {
		alert('패스워드는 영문대문자/영문소문자/숫자/특수기호중 2가지 이상을 혼합하여 입력하세요.');
		return true;
	}

	for (var i = 0 ; i < passwd.length ; i++) {
		if (passwd.charAt(i+1) != '' && passwd.charAt(i+2) != '') {
			if (passwd.charCodeAt(i) == passwd.charCodeAt(i+1) && passwd.charCodeAt(i+1) == passwd.charCodeAt(i+2)) {	//동일문자 3회 반복
				alert('동일문자를 연속3회이상 반복 하실 수 없습니다.');
				return true;
			}
			var str = passwd.charAt(i)+''+passwd.charAt(i+1)+''+passwd.charAt(i+2);

			var pattern = getPattern(str, false);

			for (var j = 0 ; j < strights.length ; j++) {
				if (pattern.exec(strights[j]) != null) {
					alert('연속된 알파벳/숫자 조합을 사용할 수 없습니다.');
					return true;
				}
			}

			//아이디와 3자 이상 중복 불가
			if (pattern.exec(usr_id) != null) {
				alert('아이디와 3자 이상 중복될 수 없습니다.');
				return true;
			}
		}
	}

	for (var i = 0 ; i < passwd.length ; i++) {
		if (passwd.charAt(i+1) != '' && passwd.charAt(i+2) != '' && passwd.charAt(i+3) != '') {
			var str = passwd.charAt(i)+''+passwd.charAt(i+1)+''+passwd.charAt(i+2) +''+ passwd.charAt(i+3);

			var pattern = getPattern(str);

			for (var j = 0 ; j < keypads.length ; j++) {
				if (pattern.exec(keypads[j]) != null) {
					alert('연속된 키보드 조합을 사용할 수 없습니다.');
					return true;
				}
			}
		}
	}
	return false;
}

/**
* <pre>
* 인수로 받은 object가 배열인지 판단 한다.
* - null이면 0을 리턴
* - 배열이 아니면 1을 리턴
* - 배열이라면 배열 길이를 리턴
* </pre>
* @param obj 검사할 form.element
* @return number (0, 1, obj.length)
*/
var isArray = function(obj){
	if(obj == null){
		return 0;
	}else {
		//alert(obj.type);
		if(obj.type == 'select-one'){
			return 1;
		}else if(obj.type == 'select-multiple'){
			return 1;
		}else{
			if(obj.length > 1){
				return obj.length;
			}else {
				return 1;
			}
		}
	}
}

/**
* dom 객체 생성
*
* td = fnCreateObj("td", "contents_area");
* --> <td class="contents_area"></td>
*
* @param typeid
* @param classid
* @return
*/
var fnCreateObj = function(typeid, classid){

	var typeobj = document.createElement(typeid);

	if( typeof(classid) != "undefined" && classid != "" )
		typeobj.className = classid;

	return typeobj;
}

/**
* input type=radio 또는 checkbox 의 value 값을 가져온다.
* @ param obj : radio 또는 checkbox Object
*/
var getRadiosValue = function(obj) {
  if(typeof(obj.length) == "undefined"){
	if(obj.checked == true){
	    return obj.value;
	}else{
		if(typeof(obj.uncheckvalue) != "undefined"){
			return obj.uncheckvalue;
		}
	}
  }else{
	for(i = 0; i < obj.length; i++) {
		if(obj[i].checked == true)
			return obj[i].value;
	}
	return "";
  }
  return "";
}

/**
* input element의 hidden type object를 생성하여 해당 parent_obj에 넣는다.
*
* @param parent_obj 생성되는 object를 넣을 부모 object object타입으로 넘겨준다.
* @param elemName element의 이름
* @param elemValue element의 값
*/
var putDomInput = function(parent_obj, elemName, elemValue){
	var input = document.createElement("input");
	input.setAttribute("type", "hidden");
	input.setAttribute("name", elemName);
	input.setAttribute("value", elemValue);
	parent_obj.appendChild(input);
}

/**
* 스트링 변환
* @param originStr 변환 대상 스트링
* @param from 바꿀 대상 스트링
* 바꿀 대상 스트링 중에 정규식 표현 문자 『  . 또는 / 또는 (  또는 )   』
* 가 들어 있을 경우 \\ (역슬래시 두개) 를 앞에 붙여서 표현한다.
* 예) var str = "12.34"; 에서 . 을 "" 로 replace 할 경우  replaceStr(str, "\\.","");
* @param to 목적 스트링
* @return string
*/
var replaceStr = function(originStr, from, to) {
	if(typeof(originStr) == "undefined") return "";
  if(typeof(originStr) != "string") originStr = String(originStr);
  return originStr.replaceAll(from, to);
}

/**
*
* <br>태그를 제외한 나머지 tag를 모두 삭제한다
*
* @param str
*            문자열
* @return 변환된 문자열
*/
var removeTag = function(str) {
	if (typeof(str) == undefined || str == "")
		return "";

	var ret = str;

	ret = replaceStr(ret, "<br>", "\n");

	ret = replaceStr(ret, "<(/)?([a-zA-Z]*)(\\s[a-zA-Z]*=[^>]*)?(\\s)*(/)?>", "");

	ret = replaceStr(ret, "\n", "<br>");
	ret = trim(ret);
	return ret;
}

/**
* <PRE>
* Scroll 이 없는 새 창을 띄운다
* </PRE>
* @param   theURL 새로 띄울 파일 이름이다
* @param   winName 새창 이름
* @param   winTitle 새창 title
* @param	width 새창 가로 크기
* @param	height 새창 세로 크기
* @param   param 추가적인 화면 argument
*/
var openNoScrollWin = function(theURL, winName, winTitle, width, height, param)
{
	var wid = (screen.width)/2 - width/2 ;
	var hei = (screen.height)/2 - height/2;
	var win = window.open(theURL + "?popupTitle=" + winTitle + "&tableWidth=" + width + param, winName, "menubar=no, scrollbars=no, resizable=no, width=" + width + ", height=" + height+ ",top=" + hei + ",left=" + wid + "");

	if(win != null)
		win.focus();
}

var goNextField = function(thisfield, nextfield, fieldmax) {
	if(thisfield.value.length == fieldmax){
		nextfield.focus();
	}
}

/**
* <PRE>
* Scroll 이 없는 새 창을 띄운다
* </PRE>
* @param   theURL 새로 띄울 파일 이름이다
* @param   winName 새창 이름
* @param   winTitle 새창 title
* @param	width 새창 가로 크기
* @param	height 새창 세로 크기
* @param   param 추가적인 화면 argument
*/
var openScrollWin = function(theURL, winName, winTitle, width, height, param)
{
	var wid = (screen.width)/2 - width/2 ;
	var hei = (screen.height)/2 - height/2;
	var win = window.open(theURL + "?popupTitle=" + winTitle + "&tableWidth=" + width + param, winName, "menubar=no, scrollbars=yes, resizable=no, width="+width+", height="+height+ ",top=" + hei + ",left=" + wid + "");

	if(win != null)
		win.focus();
}

var flashCM = function(URL,width,height,vars,bgColor,winmode, id) {
	var id=URL.split("/")[URL.split("/").length-1].split(".")[0];
	if(vars==null) vars='';
	if(bgColor==null) bgColor='transparent';
	if(winmode==null) winmode='opaque';
	document.write("	<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0' ");
	document.write("		width='"+width+"' height='"+height+"' id='"+id+"' align='middle'> ");
	document.write("	<param name='allowScriptAccess' value='always' /> ");
	document.write("	<param name='movie' 						value='"+URL+"' /> ");
	document.write("	<param name='FlashVars' 				value='"+vars+"' /> ");
	document.write("	<param name='wmode' 						value='"+winmode+"' /> ");
	document.write("	<param name='menu' 							value='false' /> ");
	document.write("	<param name='quality'						value='high' /> ");
	document.write("	<param name='bgcolor'						value='"+bgColor+"' /> ");
	document.write("	<embed src='"+URL+"' flashVars='"+vars+"' wmode='"+winmode+"' menu='false' quality='high' ");
	document.write("		bgcolor='"+bgColor+"' width='"+width+"' height='"+height+"' name='"+id+"' align='middle' ");
	document.write("		allowScriptAccess='always' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' /> ");
	document.write("	</object> ");
}

/**
* TextArea 글자수 체크
* @param	this: 체크할 객체
* 			len: 제한글자수
* ex) onKeyUp="javascript:fnChkAreaLen(this, 33);"
*/
var fnChkAreaLen = function(objTxtArea, len) {

	if(objTxtArea.value.length > len) {
		alert("입력가능한 글자수는 최대 "+len+"입니다.");
		objTxtArea.value = objTxtArea.value.substring(0,len);
		objTxtArea.focus();
	}
}

var getByteLengthRtn = function(obj, maxLen){
	var s;
	if (typeof(obj) == "string") {	//문자열
		s = obj;
	} else {
		s = obj.value;
	}
	var len = 0;
	if ( s == null ) {
		return 0;
	}
	var cutIdx;
	for(var i = 0 ; i < s.length ; i ++) {
		var c = escape(s.charAt(i));
		if ( c.length == 1 ) {
			len ++;
		} else if ( c.indexOf("%u") != -1 ) {
			len += 2;
		} else if ( c.indexOf("%") != -1 ) {
			len += c.length/3;
		}

		if(len > maxLen){
			cutIdx = i;
			break;
		}
	}
	if(len > maxLen){
		alert("한글기준"+ maxLen/2 +"글자까지 입력가능 합니다!");
		s = s.substring(0,cutIdx);
		obj.value = s;
		return false;
	}

	return true;
};

/**
 * 현재날짜
 * @returns {String}
 */
function getNowDate() {
	var d = new Date();
	var s =
	leadingZeros(d.getFullYear(), 4) + '-' +
	leadingZeros(d.getMonth() + 1, 2) + '-' +
	leadingZeros(d.getDate(), 2);
	return s;
}
function getNowDate2() {
	var d = new Date();
	var s =
	leadingZeros(d.getFullYear(), 4) + '-' +
	leadingZeros(d.getMonth() + 7, 2) + '-' +
	leadingZeros(d.getDate(), 2);
	return s;
}
function getNowDate3() {
	var d = new Date();
	var s =
	leadingZeros(d.getFullYear()+1, 4) + '-' +
	leadingZeros(d.getMonth()+1, 2) + '-' +
	leadingZeros(d.getDate(), 2);
	return s;
}

function getNowDate01() {
	var d = new Date();
	var theDay = d.getDay();

	if(theDay == 5){
	var s =
		leadingZeros(d.getFullYear(), 4) + '-' +
		leadingZeros(d.getMonth() + 1, 2) + '-' +
		leadingZeros(d.getDate()+ 3, 2);
	} else if(theDay == 6){
		var s =
			leadingZeros(d.getFullYear(), 4) + '-' +
			leadingZeros(d.getMonth() + 1, 2) + '-' +
			leadingZeros(d.getDate()+ 2, 2);
	} else {
		var s =
			leadingZeros(d.getFullYear(), 4) + '-' +
			leadingZeros(d.getMonth() + 1, 2) + '-' +
			leadingZeros(d.getDate()+ 1, 2);
	}
	return s;
}



/**
 * 현재날짜-7
 * @returns {String}
 */
function getB1Date() {
	var d = new Date();
	var s =
	leadingZeros(d.getFullYear(), 4) + '-' +
	leadingZeros(d.getMonth() , 2) + '-' +
	leadingZeros(d.getDate() , 2);
	return s;
}

function leadingZeros(n, digits) {
	var zero = '';
	n = n.toString();
	if (n.length < digits) {
		for (i = 0; i < digits - n.length; i++)
		zero += '0';
		}
	return zero + n;
}



/**
 * 현재날짜에서 - month
 * @param m
 * @returns {String}
 */
function getBeforeMonthDate(m) {
	var n = 1 ;

	if (isNaN(m)) n -= 1;
	else n -= parseFloat(m) ;

	var d = new Date();
	d.setMonth(d.getMonth() + (n));

	var s = leadingZeros(d.getFullYear(), 4) + '-' + leadingZeros(d.getMonth(), 2) + '-' + leadingZeros(d.getDate(), 2);
	return s;
}


/**
html 테그로 변환한다.
작성자 오지현 (추후 프론트는 변환하지 않기에 제외)
*/
var convertToHtmlTag = function(str) {
	if (typeof(str) == undefined || str == "")
		return "";

	var ret = str;

	/*
	ret = replaceStr(ret, "&lt;", "<");

	ret = replaceStr(ret, "&gt;", ">");
	ret = replaceStr(ret, "&nbsp;&nbsp;&nbsp;", "\t");
	ret = replaceStr(ret, "&#34;", "\"");
	ret = replaceStr(ret, "&quot;", "\"");

	ret = trim(ret);
	*/
	return ret;
}

/**
* 인풋 박스를 동적으로 생성한다
* @param name : input box이름
* @param type : input box타입
* @param value : input box 값
* @param parentId : input box삽입할 div or form id
* @return
* @author charles
*/
var createInput = function(name, type, value, parentId){
	var input = document.createElement("input");
	input.id = name;
	input.name = name;
	input.type = type;
	input.value = value;
	document.getElementById(parentId).appendChild(input);
}

/**
* 인풋 박스를 동적으로 삭제한다
* @param name : input box이름
* @param parentId : input box삭제할 div or form id
* @return
* @author charles
*/
var removeInput = function(name, parentId ) {
	var input = document.getElementsByName(name);
	var ii = input.length-1;
	for (i=ii;i>=0;i--) {
		document.getElementById(parentId).removeChild(input[i]);
	}
}

/**
* 인풋 박스를 자동으로 이동한다.
* @param name : input form.element
* @param len : input box에 입력할 문자열의 길이
* @return
* @author
*/
var autoTab = function (input,len){
	var str = input.value.length;
	var tags = document.getElementsByTagName("input")

	var i = 0;
	if(str >= len){
		for (i=0;i<tags.length-1 ;i++ )
		{
			if (tags[i].name == input.name)
			{
				tags[i+1].focus();
				return;
			}
		}
	}
}

/**
 * 셀렉트 박스의 option을 다시 설정
 * @param group    코드 그룹
 * @param selectID form.element
 * @param varCode  array[][]
 * @param selectValue 기본적으로 선택되어지는 값
 * @param firstValue
 */
function setOption(group, selectID, varCode , selectValue , firstValue){

	// select box create
	var slt = selectID;

	// 기존의 값을 삭제한다.
	for (i=slt.length ; i>=0 ; i--) {
		slt.remove(i);
	}

	// 첫번째 들어갈 항목명이 존재하면
	if (firstValue == null ) firstValue = ["","선택"];

	if (firstValue.length > 0) {
		var opt = document.createElement('OPTION');
		slt.add(opt);
		opt.value = firstValue[0];
		opt.innerText  = firstValue[1];
	}

	// option 만들기
	var oSize = varCode.length;

	for ( i=0 ; i<oSize ; i++) {

		if (group == varCode[i][0] || group == "") {
			opt = document.createElement('OPTION');
			slt.add(opt);
			opt.value 	   = varCode[i][1];
			opt.innerText  = varCode[i][2];

			if (selectValue == varCode[i][1]) {
				opt.selected = true;
			}
		}

	}
}

/**
* 세션이 끊어 졌는지를 WISEGRID_CMD 파라미터를 넘기는지 여부를 오류 발생으로 체크한다.
* @param GridObj : wisegrid
* @return cmd
* @author
*/
function getCmd(GridObj){
	try{
		var cmd = GridObj.GetParam("WISEGRID_CMD");
		return cmd;
	}catch(e){
		alert("로그인한 상태가 아니면 수행할 수 없습니다. 다시 로그인을 수행하십시요.");
		if(top.leftFrame!=null) top.location.href="COMAQ01.L01.cmd";
		return;
	}
}

/**
* chart만 있는경우 세션이 끊어 졌는지를 message 를 넘기는지 여부를 오류 발생으로 체크한다.
* @param GridObj : doc
* @return
* @author
*/
function getSessionChk(doc){
	try{
		var message = doc.documentElement.selectSingleNode("message").text;
		return;
	}catch(e){
		alert("로그인한 상태가 아니면 수행할 수 없습니다. 다시 로그인을 수행하십시요.");
		if(top.leftFrame!=null) top.location.href="COMAQ01.L01.cmd";
		return;
	}
}

/**
*	검색 : 내방객
* modal dialog
* title : 팝업 제목 (문자열)
* search : 검색에 사용할 값 (문자열)
*/
function getCMD01(title, search){
	var v_title = '';
	var v_search = '';
	if(title != undefined && title != null){
		v_title = encodeURIComponent(title);
	}
	if(search != undefined && search != null){
		v_search = encodeURIComponent(search);
	}

	return window.showModalDialog('CMD01.PL01.cmd?search='+v_search+'&title='+v_title, 'POPUP', 'dialogWidth:500px;dialogHeight:400px;status:no;scroll:no;');
}

/**
*	검색 : 임직원
* modal dialog
* title : 팝업 제목 (문자열)
* search : 검색에 사용할 값 (문자열)
*/
function getCME01(title, search){
	var v_title = '';
	var v_search = '';
	if(title != undefined && title != null){
		v_title = encodeURIComponent(title);
	}
	if(search != undefined && search != null){
		v_search = encodeURIComponent(search);
	}

	return window.showModalDialog('CME01.PL01.cmd?search='+v_search+'&title='+v_title, 'POPUP', 'dialogWidth:500px;dialogHeight:400px;status:no;scroll:no;');
}

/**
*	검색 : 부서
* modal dialog
* title : 팝업 제목 (문자열)
* search : 검색에 사용할 값 (문자열)
*/
function getCME02(title, search){
	var v_title = '';
	var v_search = '';
	if(title != undefined && title != null){
		v_title = encodeURIComponent(title);
	}
	if(search != undefined && search != null){
		v_search = encodeURIComponent(search);
	}

	return window.showModalDialog('CME02.PL01.cmd?search='+v_search+'&title='+v_title, 'POPUP', 'dialogWidth:500px;dialogHeight:400px;status:no;scroll:no;');
}

/**
*	검색 : 접견자(납품)
* modal dialog
* title : 팝업 제목 (문자열)
* search : 검색에 사용할 값 (문자열)
*/
function getCME03(title, search){
	var v_title = '';
	var v_search = '';
	if(title != undefined && title != null){
		v_title = encodeURIComponent(title);
	}
	if(search != undefined && search != null){
		v_search = encodeURIComponent(search);
	}

	return window.showModalDialog('CME03.PL01.cmd?search='+v_search+'&title='+v_title, 'POPUP', 'dialogWidth:500px;dialogHeight:400px;status:no;scroll:no;');
}

/**
*	검색 : 반출확인자
* modal dialog
* title : 팝업 제목 (문자열)
* search : 검색에 사용할 값 (문자열)
*/
function getCME04(title, search){
	var v_title = '';
	var v_search = '';
	if(title != undefined && title != null){
		v_title = encodeURIComponent(title);
	}
	if(search != undefined && search != null){
		v_search = encodeURIComponent(search);
	}

	return window.showModalDialog('CME04.PL01.cmd?search='+v_search+'&title='+v_title, 'POPUP', 'dialogWidth:500px;dialogHeight:400px;status:no;scroll:no;');
}

/**
*	검색 : 결재자
* modal dialog
* title : 팝업 제목 (문자열)
* search : 검색에 사용할 값 (문자열)
*/
function getCMX01(title, search){
	var v_title = '';
	var v_search = '';
	if(title != undefined && title != null){
		v_title = encodeURIComponent(title);
	}
	if(search != undefined && search != null){
		v_search = encodeURIComponent(search);
	}

	return window.showModalDialog('CMX01.PL01.cmd?search='+v_search+'&title='+v_title, 'POPUP', 'dialogWidth:500px;dialogHeight:360px;status:no;scroll:no;');
}



/**
*	검색 : 퇴사자
* modal dialog
* title : 팝업 제목 (문자열)
* s_txt : 검색에 사용할 값 (문자열)
*/
function getCMF01(title, search){
	var v_title = '';
	var v_search = '';
	if(title != undefined && title != null){
		v_title = encodeURIComponent(title);
	}
	if(search != undefined && search != null){
		v_search = encodeURIComponent(search);
	}

	return window.showModalDialog('CMF01.PL01.cmd?search='+v_search+'&title='+v_title, 'POPUP', 'dialogWidth:500px;dialogHeight:400px;status:no;scroll:no;');
}

/**
*	검색 : 노트북사용자
* modal dialog
* title : 팝업 제목 (문자열)
* s_txt : 검색에 사용할 값 (문자열)
*/
function getCMN01(title, search){
	var v_title = '';
	var v_search = '';
	if(title != undefined && title != null){
		v_title = encodeURIComponent(title);
	}
	if(search != undefined && search != null){
		v_search = encodeURIComponent(search);
	}

	return window.showModalDialog('CMN03.PL03.cmd?search='+v_search+'&title='+v_title, 'POPUP', 'dialogWidth:500px;dialogHeight:400px;status:no;scroll:no;');
}


/**
*	검색 : 업체
* modal dialog
* title : 팝업 제목 (문자열)
* s_txt : 검색에 사용할 값 (문자열)
*/
function getCMG01(title, search){
	var v_title = '';
	var v_search = '';
	if(title != undefined && title != null){
		v_title = encodeURIComponent(title);
	}
	if(search != undefined && search != null){
		v_search = encodeURIComponent(search);
	}

	return window.showModalDialog('CMG01.PL01.cmd?search='+v_search+'&title='+v_title, 'POPUP', 'dialogWidth:500px;dialogHeight:400px;status:no;scroll:no;');
}

/**
*	검색 : 업체(등록화면)
* modal dialog
* title : 팝업 제목 (문자열)
* s_txt : 검색에 사용할 값 (문자열)
*/
function getFRE02(title, user_Id){
	var v_title = '';
	var v_userId = '';
	if(title != undefined && title != null){
		v_title = encodeURIComponent(title);
	}
	if(user_Id != undefined && user_Id != null){
		v_userId = encodeURIComponent(user_Id);
	}
	return window.showModalDialog('FRE02.PL01.cmd?userId='+v_userId+'&title='+v_title, 'POPUP', 'dialogWidth:500px;dialogHeight:400px;status:no;scroll:no;');
}

/**
*	검색 : 동일 업체 사용자 검색
* modal dialog
* title : 팝업 제목 (문자열)
* s_txt : 검색에 사용할 값 (문자열)
*/
function getCMS01(title, search){
	var v_title = '';
	var v_search = '';
	if(title != undefined && title != null){
		v_title = encodeURIComponent(title);
	}
	if(search != undefined && search != null){
		v_search = encodeURIComponent(search);
	}

	return window.showModalDialog('CMS01.PL01.cmd?search='+v_search+'&title='+v_title, 'POPUP', 'dialogWidth:500px;dialogHeight:400px;status:no;scroll:no;');
}

/**
*	검색 : 자재코드 리스트
* modal dialog
* title : 팝업 제목 (문자열)
* s_txt : 검색에 사용할 값 (문자열)
*/
function getCMH01(title, search){
	var v_title = '';
	var v_search = '';

	if(title != undefined && title != null){
		v_title = encodeURIComponent(title);
	}
	if(search != undefined && search != null){
		v_search = encodeURIComponent(search);
	}

	return window.showModalDialog('CMH01.PL01.cmd?search='+v_search+'&title='+v_title, 'POPUP', 'dialogWidth:500px;dialogHeight:400px;status:no;scroll:no;');
}

/**
*	검색 : 인수자
* modal dialog
* title : 팝업 제목 (문자열)
* search : 검색에 사용할 값 (문자열)
*/
function getCMI01(title, search){
	var v_title = '';
	var v_search = '';
	if(title != undefined && title != null){
		v_title = encodeURIComponent(title);
	}
	if(search != undefined && search != null){
		v_search = encodeURIComponent(search);
	}

	return window.showModalDialog('CMI01.PL01.cmd?search='+v_search+'&title='+v_title, 'POPUP', 'dialogWidth:500px;dialogHeight:400px;status:no;scroll:no;');
}

/**
*	검색 : prno
* modal dialog
* title : 팝업 제목 (문자열)
* flag : 선택자
*/
function getCMT01(title, flag){
	var v_title = '';
	var v_flag = '';
	if(title != undefined && title != null){
		v_title = encodeURIComponent(title);
	}
	if(flag != undefined && flag != null){
		v_flag = encodeURIComponent(flag);
	}
	return window.showModalDialog('CMT01.PL01.cmd?flag='+v_flag+'&title='+v_title, 'POPUP', 'dialogWidth:600px;dialogHeight:400px;status:no;scroll:no;');
}

/**
*	검색 : 우편번호
* modal dialog
* ZIPCODE : 우편번호
* ADDR : 주소
*/
function getPOST(){
	return window.showModalDialog('FRE01.PL01.cmd', 'POPUP', 'dialogWidth:500px;dialogHeight:400px;status:no;scroll:no;');
}
/**
*	툴팁
*/
Ext.apply(Ext.QuickTips.getQuickTip(), {
	autowidth :true,
    showDelay: 500,      // Show 50ms after entering target
    trackMouse: true
});
function addQtip(val, metadata, record, rowIndex,colIndex, store){
	metadata.attr = 'ext:qtip="'+ val + '"';
	return val;
}
function user_info_popup(){
	window.showModalDialog('./USD01.L01.cmd', window, 'dialogWidth:722px;dialogHeight:410px;status:no;scroll:no;');
	//window.open('./USD01.L01.cmd', 'POPUP', 'dialogWidth:722px;dialogHeight:410px;status:no;scroll:no;');
}
function logOut(){
	Ext.Msg.confirm("Confirm", "로그아웃 하시겠습니까?", function(btn, text) {
		if(btn == "no") {
			return;
		} else {
			Ext.Ajax.request({
				url: 'CMB01.S01.cmd',
				method: 'POST',
				// 성공했을때 처리
				success: function(result, request){
					var response = result.responseXML;
				   	var Message = cfnGetSvcMessage(response,"Message");
				   	var Code = cfnGetSvcMessage(response,"Code");
				   	var Success = cfnGetSvcMessage(response,"Success");
					// 프로그래스바를 없앤다.
				   	if(Success == "true") {
				   		Ext.MessageBox.alert("Message", Message,function(btn, text){
				   		    if (btn == 'ok'){
				   		    	top.location.href = "CMK01.L01.cmd";
				   		    }
				   		});
				   	}else{
				   		Ext.MessageBox.alert("Message", Message);
				   	}
				}
			});
		}
	});
}
function logOut_Directly(){
	Ext.Ajax.request({
		url: 'CMB01.S01.cmd',
		method: 'POST',
		// 성공했을때 처리
		success: function(result, request){
			var response = result.responseXML;
		   	var Message = cfnGetSvcMessage(response,"Message");
		   	var Code = cfnGetSvcMessage(response,"Code");
		   	var Success = cfnGetSvcMessage(response,"Success");
			// 프로그래스바를 없앤다.
		   	if(Success == "true") {
		   		top.location.href = "CMK01.L01.cmd";
		   	}else{
		   		Ext.MessageBox.alert("Message", Message);
		   	}
		}
	});
}