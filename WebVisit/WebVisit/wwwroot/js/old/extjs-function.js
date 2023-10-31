Number.prototype.dateLpad0 = function() { return (this > 9 ? "" : "0")+this; };
	 
/*==============================================================================
 * Function : 처리가 진행중일 경우 이벤트를 취할수 없도록 한다.
 * return :
 * examples  :
 *       setLoading(true);  ,   setLoading(false);
==============================================================================*/
function setLoading(fg){
	if(fg) {
		LOAD_STAT = true;
	    document.body.style.cursor = "wait";
	} else {
		LOAD_STAT = false;
	    document.body.style.cursor = "default";
	    window.status = "완료";
	}
}

document.onkeydown = eventWatch;
document.onmousedown = eventWatch;

function eventWatch(){
    /**
	if(LOAD_STAT){
		alert("처리중입니다!!\n잠시만 기다려주십시요.");
		return false;
	} else {
		return true;
	}
	**/
}

/*==============================================================================
 * 입력한 값이 영문과 숫자를 혼용하는지 검사
 *
 * @return	boolean				혼용이면 true, 아니면 false
============================================================================== */

function chkIdValidation(str) {
	 
	 res1 = (/[a-z]/i).chkIdValidation(str); //영문이 있는지
	 res2 = (/[0-9]/).chkIdValidation(str); //숫자가 있는지
	 res3 = (/^[0-9a-z]*$/i).chkIdValidation(str); //영문, 숫자 이외엔 없는지
	 res4 = false;
	 if(str.length > 8 && str.length < 12){	// 길이가 8~12자 사이
	 res4=true;
	 }
	 
	 if(res1&&res2&&res4){
	 return true;
	 }else{
	 return false;
	 }
}


/*==============================================================================
 * key-event에서 입력키의 코드값을 추출하여 반환
 *
 * @return	int				키입력의 코드값(10진수)
============================================================================== */
function getKeyValue() {
	//return (ie4) ? event.keyCode : event.which;
	return event.keyCode;
}

/*==============================================================================
 * key-event에서 키입력이 'Enter(13)'키인지를 검사
 *
 * @return	boolean		키입력이 'Enter' 키이면 true, 그외 false
============================================================================== */
function isEnterKeyEvent() {
	return getKeyValue() == 13;
}

/*==============================================================================
 * key-event에서 키입력이 숫자키인지를 검사.(0(48)~9(57))
 *
 * @return	boolean		키입력이 숫자키이면 true, 그외 false
============================================================================== */
function isNumberKeyEvent() {
	var	keyValue	= getKeyValue();
	
	return (keyValue >= 48 && keyValue <= 57);
}

/*==============================================================================
 * key-event에서 키입력이 기능키인지를 검사.
 *
 * '\b'(8), '\t'(9),
 * 'Shift'(16), 'Ctrl'(17), 'Alt'(18), 'CapsLock'(20)
 * 'PageUp'(33), 'PageDown'(34), 'End'(35), 'Home'(36),
 * '←'(37), '↑'(38), '→'(39), '↓'(40),
 * 'Insert'(45), 'Delete'(46)
 * 'NumLock'(144)
 *
 * @return	boolean		키입력이 기능키이면 true, 그외 false
============================================================================== */
function isFuctionKeyEvent() {
	var	keyValue			= getKeyValue();
	var	functionKeys	= [ 8,9,16,17,18,20,33,34,35,36,37,38,39,40,45,46,144 ];

	for (var i=0; i<functionKeys.length; i++) {
		if (keyValue == functionKeys[i])
			return true;
	}

	return false;
}

/*==============================================================================
 * key-event에서 키입력이 'Enter(13)'키인 경우 key 입력을 무시
============================================================================== */
function ignoreEnterKey() {
	if (isEnterKeyEvent()) {
		event.returnValue		= false;
	}
}

/*==============================================================================
 * 『onKeyPress』Event에서 숫자와 기능키를 제외한 기타 다른 키입력을 무시한다.
 * '+'(43), '-'(45), '.'(46)
 *
 * @param		field			TextField
============================================================================== */
function makeNumberOnly(field) {
	var	keyValue		= getKeyValue();

	event.returnValue	= (field.value.length == 0 && (keyValue == 43 || keyValue == 45))
										  || (field.value.length > 0 && field.value != '-' && field.value != '+' && field.value.indexOf('.') == -1 && keyValue == 46)
										  || isNumberKeyEvent();
}

/*==============================================================================
 * 『onKeyDown』Event에서 Element의 value 속성의 byte길이를
 * 주어진 길이만큼으로 제한한다.
 *
 * @param		elem		TextField 또는 TextArea
============================================================================== */
function limitBytesLength(elem, maxLen) {
	event.returnValue		= (isFuctionKeyEvent() || getBytesLength(elem.value) < maxLen);
}


/*==============================================================================
 * 문자열 처음의 whitespace 문자들을 제거
 *
 * @param		str			원본 문자열
 * @return	String	왼쪽 whitespace 문자들이 제거된 문자열
============================================================================== */
function leftTrim(str) {
	var	regExp		= /^\s+/;

	return (regExp.exec(str) != null) ? RegExp.rightContext : str;
}

/*==============================================================================
 * 문자열 마지막의 whitespace 문자들을 제거
 *
 * @param		str			원본 문자열
 * @return	String	오른쪽 whitespace 문자들이 제거된 문자열
==============================================================================*/
function rightTrim(str) {
	var	regExp		= /\s+$/;

	return (regExp.exec(str) != null) ? RegExp.leftContext : str;
}

/*==============================================================================
 * 문자열 양쪽 끝의 whitespace 문자들을 제거
 *
 * @param		str			원본 문자열
 * @return	String	양쪽 끝의 whitespace 문자들이 제거된 문자열
============================================================================== */
function trim(str) {
	return rightTrim(leftTrim(str));
}

/*==============================================================================
 * 문자열의 byte 길이를 반환
 *
 * @param		str		Byte 길이를 계산할 문자열
 * @return	int		문자열의 byte 길이
============================================================================== */
function getBytesLength(str) {
	return str.length + escape(str).split('%u').length - 1;
}

function getLength(str) {
	return str.length;
}

/*==============================================================================
 * 문자열이 E-mail 주소로 유효한지 검사
 *
 * @param		addrStr		주소 문자열
 * @return	boolean		E-mail 주소로 유효한 문자열이면 true, 그외 false
============================================================================== */
function isValidEmail(emailAddr) {
	//var	regExp		= /^[\w._-]+@\w+[-.]\w+[.]?\w+$/;
	//return regExp.test(emailAddr);
	
	return true;
}
	

/*==============================================================================
 * 문자열이 유효한 숫자형이면서 부호(+, -)를 포함하는지를 검사
 *
 * @param		numStr		숫자형 문자열
 * @return	boolean		유효한 숫자형이면서 부호를 포함하고 있으면 true, 그외 false
============================================================================== */
function isSigned(numStr) {
	var	regExp		= /^[+-][0-9]+[.]?[0-9]*[0-9]$/;

	return regExp.test(numStr);
}

/*==============================================================================
 * 문자열이 순수하게 숫자로만 이루어져 있는지를 검사(부호나 소수점도 포함해서는 안된다.)
 *
 * @param		numStr		숫자형 문자열
 * @return	boolean		순수한 숫자형이면 true, 그외 false
==============================================================================*/
function isPureNumber(numStr) {
	var	regExp		= /^[0-9]+$/;

	return regExp.test(numStr);
}

/*==============================================================================
 * 문자열이 정수형인지를 검사
 *
 * 1. 처음에 +, - 부호를 생략하거나 넣을 수 있다 : ^[+-]?
 * 2. 0에서 9까지 숫자가 1번 이상 포함된다 : [0-9]+
 *
 * @param		numStr		숫자형 문자열
 * @return	boolean		정수형이면 true, 그외 false
============================================================================== */
function isInteger(numStr) {
	var	regExp		= /^[+-]?[0-9]+$/;

	return regExp.test(numStr);
}

/*==============================================================================
 * 문자열이 실수형인지를 검사
 *
 * 1. 처음에 +, - 부호를 생략하거나 넣을 수 있다 : ^[+-]?
 * 2. 0에서 9까지 숫자가 1번 이상 포함된다 : [0-9]+
 * 3. 소수점을 넣을 수 있다 : [.]?
 * 4. 소수점 이하 자리에 0에서 9까지 숫자가 올 수 있다 : [0-9]*
 * 5. 마지막은 숫자로 끝나야 한다 : [0-9]$
 *
 * @param		numStr		숫자형 문자열
 * @return	boolean		실수형이면 true, 그외 false
============================================================================== */
function isFloat(numStr) {
	var	regExp		= /^[+-]?[0-9]*[.]?[0-9]*[0-9]$/;
	
	return regExp.test(numStr);
}


/*==============================================================================
 * 숫자형 문자열에 자리수 ','를 추가
 *
 * @param		numStr		숫자형 문자열
 * @return	String		','가 추가된 숫자형 문자열
============================================================================== */
function addComma(numStr) {
	if (!isFinite(numStr))
		return numStr;

	var	body	= String(Number(numStr));
	body			= (body.indexOf('-') > -1 || body.indexOf('+') > -1) ? body.substring(1) : body;
	body			= (body.indexOf('.') > -1) ? body.substring(0, body.indexOf('.')) : body;

	var	head	= (numStr == body) ? "" : numStr.substring(0, numStr.indexOf(body));
	var	tail	= (numStr == body) ? "" : numStr.substring(numStr.indexOf(body) + body.length);

	while (body.length > 3) {
		tail		= ',' + body.substring(body.length - 3) + tail;
		body		= body.substring(0, body.length - 3);
	}

	return head + body + tail;
}

/*==============================================================================
 * ','가 포함된 숫자형 문자열에서 ','를 제거
 *
 * @param		numStr		숫자형 문자열
 * @return	String		','가 제거된 숫자형 문자열
============================================================================== */
 function removeComma(numStr) {
	if (numStr.length == 0)
		return numStr;

	var	tokens		= numStr.split(",");
	var	newStr		= '';
	for (var i=0; i<tokens.length; i++) {
		newStr	+= tokens[i];
	}

	return newStr;
}

/*==============================================================================
 * '.'가 포함된 DATE형 문자열에서 '.'를 제거
 *
 * @param		numStr		숫자형 문자열
 * @return	String		'.'가 제거된 DATE형 문자열
============================================================================== */
 function removePoint(numStr) {
 
	if (numStr.length == 0) {
		return numStr;
	}

	var	tokens		= numStr.split(".");
	var	newStr		= '';
	for (var i=0; i<tokens.length; i++) {
		newStr	+= tokens[i];
	}

	return newStr;
}
/*==============================================================================
 * '-'가 포함된 DATE형 문자열에서 '.'를 제거
 *
 * @param		numStr		숫자형 문자열
 * @return	String		'-'가 제거된 DATE형 문자열
============================================================================== */
 function removeMa(numStr) {
 
	if (numStr.length == 0) {
		return numStr;
	}

	var	tokens		= numStr.split("-");
	var	newStr		= '';
	for (var i=0; i<tokens.length; i++) {
		newStr	+= tokens[i];
	}

	return newStr;
}

/*===================================================================================
* Form객체에 포함된 모든 TextField 및 TextArea의 value를 trim한다.
*
* @param		form		Form 객체
====================================================================================*/
function trimTextValue(form) {
	
	var elements	= form.elements;	
	
	if(elements) {
		for (var i=0; i<elements.length; i++) {
			if (elements[i].type != null && (elements[i].type == 'text' || elements[i].type == 'textarea'))
				elements[i].value	= rightTrim(elements[i].value);
		}
	}
}

/*===================================================================================
 * 날짜형 문자열이 포맷 문자열의 기준에 유효하며, 동시에 날짜값에 유효한 숫자를 사용했는지를 검사.
 * DateFormat은 J2SE Time Format Syntax 중 극히 제한적인 부분만 사용하여야 하며,
 * 사용가능한 포맷은 아래와 같다.
 *
 * 1. 년 : 'yyyy'
 * 2. 월 : 'MM'
 * 3. 일 : 'dd'
 * 4. 시 : 'HH'
 * 5. 분 : 'mm'
 * 6. 초 : 'ss'
 * (Example : 'yyyy-MM-dd HH:mm:ss')
 *
 * @param		dateFormat	날짜포맷 문자열
 * @param		dateStr			날짜형 문자열
 * @return	boolean			유효한 날짜형 문자열이면 true, 그외 false
=================================================================================== */
function isDate(dateFormat, dateStr) {
	var	letterRegExp	= /^[yMdHms]$/;
	var	regSource			= '^';

	for (var i=0; i<dateFormat.length; i++) {
		if (letterRegExp.test(dateFormat.charAt(i)))
			regSource	+= '[0-9]';
		else
			regSource	+= '[' + dateFormat.charAt(i) + ']';
	}
	regSource	+= '$';

	if (!(new RegExp(regSource)).test(dateStr))
		return false;		// 포맷과 일치하지 않는 경우


	var	yearIdx					= dateFormat.indexOf('yyyy');
	var	monthIdx				= dateFormat.indexOf('MM');
	var	dayOfMonthIdx		= dateFormat.indexOf('dd');
	var	hourIdx					= dateFormat.indexOf('HH');
	var	minuteIdx				= dateFormat.indexOf('mm');
	var	secondIdx				= dateFormat.indexOf('ss');

	var	year		= -1;
	var	month		= -1;

	if (yearIdx > -1) {
		year	= Number(dateStr.substring(yearIdx, yearIdx + 4));
		if (year < 1970)
			return false;		// 년 : 1970년 1월 1일 0시 이후의 시간만 유효
	}

	if (monthIdx > -1) {
		month	= Number(dateStr.substring(monthIdx, monthIdx + 2));
		if (month < 1 || month > 12)
			return false;		// 월 : 1~12월
	}

	if (dayOfMonthIdx > -1) {
		var dateRange		= [ 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 ];
		if (year != -1 && year%4 == 0 && (year%100 != 0 || year%400 == 0))
			dateRange[1] = 29;

		var	dayOfMonth	= Number(dateStr.substring(dayOfMonthIdx, dayOfMonthIdx + 2));
		if (dayOfMonth < 1 || dayOfMonth > 31 || (month != -1 && dayOfMonth > dateRange[month-1]))
			return false;		// 일 : 1~31일, 각월의 최대일수 보다 클 수 없음
	}

	if (hourIdx > -1) {
		var	hour	= Number(dateStr.substring(hourIdx, hourIdx + 2));
		if (hour > 23)
			return false;		// 시 : 0~23시
	}

	if (minuteIdx > -1) {
		var	minute	= Number(dateStr.substring(minuteIdx, minuteIdx + 2));
		if (minute > 59)
			return false;		// 분 : 0~59분
	}

	if (secondIdx > -1) {
		var	second	= Number(dateStr.substring(secondIdx, secondIdx + 2));
		if (second > 59)
			return false;		// 초 : 0~59초
	}

	return true;
}

/*===================================================================================
 * 날짜포맷의 예를 문자열로 생성하여 반환(예 : 2001년 1월 1일 23시 1분 59초)
 *
 * @param		dateFormat		날짜포맷
 * @return	String				포맷에 부합하는 날짜예
=================================================================================== */
function makeDateExample(dateFormat) {
	var	dateStr			= '';
	var	example			= dateFormat;

	var	regExp			= new RegExp('yyyy');
	if (regExp.test(dateFormat)) {
		dateStr		+= '2001년 ';
		example		=  example.replace(regExp, '2001');
	}

	regExp.compile('MM');
	if (regExp.test(dateFormat)) {
		dateStr		+= '1월 ';
		example		=  example.replace(regExp, '01');
	}

	regExp.compile('dd');
	if (regExp.test(dateFormat)) {
		dateStr		+= '1일 ';
		example		=  example.replace(regExp, '01');
	}

	regExp.compile('HH');
	if (regExp.test(dateFormat)) {
		dateStr		+= '23시 ';
		example		=  example.replace(regExp, '23');
	}

	regExp.compile('mm');
	if (regExp.test(dateFormat)) {
		dateStr		+= '1분 ';
		example		=  example.replace(regExp, '01');
	}

	regExp.compile('ss');
	if (regExp.test(dateFormat)) {
		dateStr		+= '59초 ';
		example		=  example.replace(regExp, '59');
	}

	return dateStr + '==> ' + example;
}
	
/*=====================================================================================================
 * Form 입력 데이터의 유효성을 일괄적으로 검사하며, 실패시 alert창을 띄우고 focus를 준다.
 * Html의 input 태그안에 다음의 속성을 추가하여 JavaScript에서 이 정보를 활용한다.
 * (속성[적용객체 type] : 설명)
 *
 * 1. dispName      [all]                    : Element를 표현할 이름
 * 2. notNull       [all]                    : 필수입력항목 여부
 * 3. dataType      [text]                   : number|integer|float|date|email
 * 4. format        [text]                   : date 타입의 포맷문자열
 * 5. regExp        [text|textarea|password] : value로 허용할 문자열의 정규식
 * 5. notRegExp     [text|textarea|password] : value로 허용할 안할 문자열의 정규식
 * 6. len           [text|textarea|password] : 입력문자열의 규정된 byte 단위길이
 * 7. minLen        [text|textarea|password] : 입력문자열의 byte 단위의 최소길이
 * 8. maxLen        [text|textarea|password] : 입력문자열의 byte 단위의 최대길이
 * 9. minValue      [text]                   : 입력값의 최소값
 *10. maxValue      [text]                   : 입력값의 최대값
 *11. fileNameMaxLen[file]                   : 입력파일명의 최대길이(확장자 포함)
 *12. fucName		[text]					 : function명
 *
 * @param		form				Form 객체
 * @return	boolean			모든 데이터가 유효하면 true, 그외 false
=======================================================================================================*/
function isValidForm() {
	var frm = document.forms;
	
	for( var ff = 0 ; ff < frm.length ; ff++ ){
		if(!isValidFormDetail(frm[ff], "Y")) return false;
	}
	
	return true;
}
function isValidFormDetail(form, frmYn) {

	trimTextValue(form);
	
	for (i=0; i<form.elements.length; i++) {
		var	obj		= form.elements[i];
		
		if (obj.type == null)
			continue;

		var	type	= (obj.type.indexOf('select') == 0) ? 'select' : obj.type;
		var	check	= true;
		var	msg;
		
		var msgCode;
		var params;
		var gubun = "user";

		/************** 필수 입력항목의 입력여부 검사 **************/
		if (obj.notNull != null && frmYn != "Y") {
			switch (type) {
				case 'radio'    :
					if (fetchRadioValue(form, obj.name) == null)
						check		= false;
					break;
				case 'select'   :
				case 'file'     :
				case 'password' :
				case 'textarea' :
				case 'text'     :
					if (obj.value.length == 0)
						check		= false;
					break;
			}

			if (!check) {
				var	action	= (type == 'radio' || type == 'select') ? '선택' : '입력';

				//msgCode = (type == 'radio' || type == 'select') ? "MSG0010" : "MSG0014";
				//params = ["「" + obj.dispName + "」"];
				
				//사용자 입력 에러 시표시(공통)
				//showPopup(usererrorUrl, msgCode, params[0], gubun);

				alert('「' + obj.dispName + '」란을 ' + action + '하세요.');
				
				obj.focus();
				if (event != null)
					event.returnValue	= false;
				return false;
			}
		}
		/******************************************************/


	
		/************ 대문자로 변경 ***********
		switch (type) {
			case 'file'     :
			case 'password' :
			case 'textarea' :
			case 'text'     :
				if (obj.value.length == 0) {
					continue;
				}
				
				if(obj.notUpper == null) {
					obj.value = obj.value.toUpperCase()
				}
				
				break;
				
			default :
				continue;
		}
		/******************************************************/
		

		/************ 유효성 검사가 필요없는 경우 Skip ************/
		switch (type) {
			case 'file'     :
			case 'password' :
			case 'textarea' :
			case 'text'     :
				if (obj.value.length == 0)
					continue;
				break;
			default :
				continue;
		}
		/******************************************************/


		/************* 정규식이 주어졌을 경우 이를 검사 *************/
		if (obj.regExp != null && !(new RegExp(obj.regExp)).test(obj.value)) {
			//var	errMsg	= (obj.type == 'file') ? '「' + obj.dispName + '」의 파일명 또는 파일경로에 특수문자가 포함되었습니다.' : '「' + obj.dispName + '」입력값이 유효하지 않습니다.';

			msgCode = (obj.type == 'file') ? "MSG0228" : "MSG0013";
			params  = (obj.type == 'file') ? ["「" + obj.dispName + "」"] : ["「" + obj.dispName + "」입력값이 유효하지 않습니다.「" + obj.dispName + "」"];
		
			//사용자 입력 에러 시표시(공통)
			//showPopup(usererrorUrl, msgCode, params[0], gubun);

			alert(errMsg);
			obj.focus();
			obj.select();
			if (event != null)
				event.returnValue	= false;
			return false;
		}
		

		/******************** data 타입 검사 ********************/
		if (obj.dataType != null) {
			var	dateFormat	= (obj.format != null) ? obj.format : 'yyyy.MM.dd';
			switch (obj.dataType) {
				case 'number'   :
					check		= isPureNumber(removeComma(obj.value));
					msgCode		= "MSG0053";	//{1}만 입력이 가능합니다.
					params		= ["「" + obj.dispName + "」란은 숫자"];
					msg			= '「' + obj.dispName + '」란은 숫자만 입력이 가능합니다.';
					break;
				case 'integer'  :
					check		= isInteger(removeComma(obj.value));
					msgCode		= "MSG0053";	//{1}만 입력이 가능합니다.
					params		= ["「" + obj.dispName + "」란은 정수형"];
					msg			= '「' + obj.dispName + '」란은 정수형만 입력이 가능합니다.';
					break;
				case 'float'    :
					check		= isFloat(removeComma(obj.value));
					msgCode		= "MSG0053";	//{1}만 입력이 가능합니다.
					params		= ["「" + obj.dispName + "」란은 실수형"];
					msg			= '「' + obj.dispName + '」란은 실수형만 입력이 가능합니다.';
					break;
				case 'date'     :
					check		= isDate(dateFormat, obj.value);

					if(frmYn == "Y")
						check		= true; // 공통에서 에러 처리로 막아둔다.
					
					msgCode		= "MSG0053";	//{1}만 입력이 가능합니다.
					params		= ["「" + obj.dispName + "」란은 날짜형식"];
					msg			= '「' + obj.dispName + '」란은 날짜형식만 입력이 가능합니다.\n\n'
					//				  + ' 예) ' + makeDateExample(dateFormat);
					break;
				case 'email'    :
					check		= isValidEmail(obj.value);
					msgCode		= "MSG0053";	//{1}만 입력이 가능합니다.
					params		= ["「" + obj.dispName + "」란은 e-mail형식"];
					msg			= '「' + obj.dispName + '」란은 e-mail형식만 입력이 가능합니다.' + '\n\n'
					//				  + ' 예) abcd@yahoo.co.kr';
					break;
			}

			if (check && type == 'text') {
				switch (obj.dataType) {
					case 'number'  :
					case 'integer' :
					case 'float'   :
						if (obj.minValue != null && Number(obj.value) < Number(obj.minValue)) {
							check		= false;
							msgCode		= "MSG0154";	//{1}을/를 {2} 이상으로 입력하십시요.
							params		= ["「" + obj.dispName + "」;" + obj.minValue, obj.minValue];
							msg			= '「' + obj.dispName + '」란은 ' + obj.minValue + ' 이상의 값을 입력하셔야 합니다.';
						} else if (obj.maxValue != null && Number(obj.value) > Number(obj.maxValue)) {
							check		= false;
							msgCode		= "MSG0154";	//{1}을/를 {2} 이상으로 입력하십시요.
							params		= ["「" + obj.dispName + "」;"+obj.maxValue, obj.maxValue];
							msg			= '「' + obj.dispName + '」란은 ' + obj.maxValue + ' 이하의 값을 입력하셔야 합니다.';
						} else {
							obj.value = addComma(obj.value);
						}
						break;
				}
			}

			if (!check) {
				alert(msg);
				
				//사용자 입력 에러 시표시(공통)
				//showPopup(usererrorUrl, msgCode, params[0], gubun);
				
				obj.focus();
				obj.select();
				if (event != null)
					event.returnValue	= false;
				return false;
			}
		}
		/******************************************************/

		/**************** 입력문자열의 길이를 검사 ****************/
		var	engLen, korLen;		
		switch (type) {			
			case 'textarea' :
				if (getBytesLength(obj.value) < Number(obj.minLen)) {
					check		= false;
					engLen	= Number(obj.minLen);
					korLen	= Math.floor(engLen / 2);
					msgCode		= "MSG0107";	//{1}을/를 입력하여 주십시오.
					params		= ["영문 또는 숫자 " + engLen + "자(한글 " + korLen + "자) 이상으로 「" + obj.dispName + "」"];
					msg			= '「' + obj.dispName + '」란은 영문 또는 숫자 ' + engLen + '자(한글 ' + korLen + '자) 이상으로 작성하셔야 합니다.';
				} else if (obj.maxLen != null && getBytesLength(obj.value) > Number(obj.maxLen)) {
					check		= false;
					engLen	= Number(obj.maxLen);
					korLen	= Math.floor(engLen / 2);
					msgCode		= "MSG0107";	//{1}을/를 입력하여 주십시오.
					params		= ["영문 또는 숫자 " + engLen + "자(한글 " + korLen + "자) 이내로 「" + obj.dispName + "」"];
					msg			= '「' + obj.dispName + '」란은 영문 또는 숫자 ' + engLen + '자(한글 ' + korLen + '자) 이내로 작성하셔야 합니다.';
				}
			case 'text'     :
			
				var objval = obj.value;
				
				switch (obj.dataType) {
					case 'number'  :
					case 'integer' :
					case 'float'   :
						objval = removeComma(objval);
						break;
				}
								
				if (obj.len != null && getLength(objval) != Number(obj.len)) {
					check		= false;
					engLen	= Number(obj.len);
					//korLen	= Math.floor(engLen / 2);
					korLen	= engLen;
					msgCode		= "MSG0107";	//{1}을/를 입력하여 주십시오.
					params		= ["영문 또는 숫자 " + engLen + "자(한글 " + korLen + "자)로 「" + obj.dispName + "」"];
					msg			= '「' + obj.dispName + '」란은 영문 또는 숫자 ' + engLen + '자(한글 ' + korLen + '자)로 작성하셔야 합니다.';
				} else if (obj.minLen != null && getLength(objval) < Number(obj.minLen)) {
					check		= false;
					engLen	= Number(obj.minLen);
					//korLen	= Math.floor(engLen / 2);
					korLen	= engLen;
					msgCode		= "MSG0107";	//{1}을/를 입력하여 주십시오.
					params		= ["영문 또는 숫자 " + engLen + "자(한글 " + korLen + "자) 이상으로 「" + obj.dispName + "」"];
					msg			= '「' + obj.dispName + '」란은 영문 또는 숫자 ' + engLen + '자(한글 ' + korLen + '자) 이상으로 작성하셔야 합니다.';
				} else if (obj.maxLen != null && getLength(objval) > Number(obj.maxLen)) {
					check		= false;
					engLen	= Number(obj.maxLen);
					//korLen	= Math.floor(engLen / 2);
					korLen	= engLen;
					msgCode		= "MSG0107";	//{1}을/를 입력하여 주십시오.
					params		= ["영문 또는 숫자 " + engLen + "자(한글 " + korLen + "자) 이내로 「" + obj.dispName + "」"];
					msg			= '「' + obj.dispName + '」란은 영문 또는 숫자 ' + engLen + '자(한글 ' + korLen + '자) 이내로 작성하셔야 합니다.';
				}
				break;
			case 'password' :
				if (obj.minLen != null && getBytesLength(obj.value) < Number(obj.minLen)) {
					check		= false;
					msgCode		= "MSG0107";	//{1}을/를 입력하여 주십시오.
					params		= [obj.minLen + "자 이상으로 「" + obj.dispName + "」"];
					msg			= '「' + obj.dispName + '」란은 ' + obj.minLen + '자 이상으로 작성하셔야 합니다.';
				} else if (obj.maxLen != null && getBytesLength(obj.value) > Number(obj.maxLen)) {
					check		= false;
					msgCode		= "MSG0107";	//{1}을/를 입력하여 주십시오.
					params		= [obj.minLen + "자 이내로 「" + obj.dispName + "」"];
					msg			= '「' + obj.dispName + '」란은 ' + obj.maxLen + '자 이내로 작성하셔야 합니다.';
				}
				break;
			case 'file' :
				var	fileName	= obj.value.substring(obj.value.lastIndexOf('\\') + 1);
				if (obj.fileNameMaxLen != null && getBytesLength(fileName) > obj.fileNameMaxLen) {
					check		= false;
					engLen	= Number(obj.fileNameMaxLen);
					korLen	= Math.floor(engLen / 2);
					msgCode		= "MSG0107";	//{1}을/를 입력하여 주십시오.
					params		= ["확장자를 포함하여 영문 또는 숫자 " + engLen + "자(한글 " + korLen + "자) 이내로 「파일명」"];
					msg			= '「파일명」은 확장자를 포함하여 영문 또는 숫자 ' + engLen + '자(한글 ' + korLen + '자) 이내이어야 합니다.';
				}
				break;
		}

		if (!check) {
			alert(msg);
			
			//사용자 입력 에러 시표시(공통)
			//showPopup(usererrorUrl, msgCode, params[0], gubun);
				
			obj.focus();
			obj.select();
			if (event != null)
				event.returnValue	= false;
			return false;
		}
		/******************************************************/
		

	}
	return true;
}

/*===================================================================================
 * 해당 문자열에서 대상문자열을 변환 문자열로 변경하여 결과를 Return
 *
 * @param	str : 원본 String
 * @param	s   : 대상 문자열
 * @param	d   : 변환 문자열
 * @return	변환된 String
=================================================================================== */
function replaceStr(str, s, d) {
	
//	re = /\./g;
//	return str1.replace(re, "");

	var i=0;
	while(i>-1) {
		i = str.indexOf(s);
		str = str.substr(0,i) + d + str.substr(i+1, str.length);
	}
	return str;
}

/*===================================================================================
 * 날짜에 점(Point)을 추가한다.
 *
 * @param	str : 원본 String
 * @return	변환된 String
=================================================================================== */
function insertPoint(str) {
	if(str.length == 8) {
		var yy = str.substring(0, 4);
		var mm = str.substring(4, 6);
		var dd = str.substring(6, 8);
		
		return yy+"."+mm+"."+dd;
	} else {
		return str;
	}
}
/*===================================================================================
 * 날짜에 점(Point)을 추가한다.
 *
 * @param	str : 원본 String
 * @return	변환된 String
=================================================================================== */
function insertMa(str) {
	if(str.length == 8) {
		var yy = str.substring(0, 4);
		var mm = str.substring(4, 6);
		var dd = str.substring(6, 8);
		
		return yy+"-"+mm+"-"+dd;
	} else {
		return str;
	}
}

/*===================================================================================
 * 숫자를 날짜형태로 변환
 *
 * @param		dateFormat		날짜포맷
 * @return	String				포맷에 부합하는 날짜예
=================================================================================== */
function makeDate(str, dateFormat) {
			
	if(str == "") return "";
	
	var	dateStr			= '';
	var	example			= dateFormat;
	var tmpStr          = '';
	
	var	regExp			= new RegExp('yyyy');
	if (regExp.test(dateFormat)) {
		tmpStr      =  str.substring(0, 4)	
		if(!tmpStr) tmpStr = "0000";
		
		example		=  example.replace(regExp, tmpStr);
	}

	regExp.compile('MM');
	if (regExp.test(dateFormat)) {
	
		tmpStr      =  str.substring(4, 6)	
		if(!tmpStr) tmpStr = "00";
		
		example		=  example.replace(regExp, tmpStr);
	}

	regExp.compile('dd');
	if (regExp.test(dateFormat)) {
	
		tmpStr      =  str.substring(6, 8)	
		if(!tmpStr) tmpStr = "00";
		
		example		=  example.replace(regExp, tmpStr);
	}

	regExp.compile('HH');
	if (regExp.test(dateFormat)) {
	
		tmpStr      =  str.substring(8, 10)	
		if(!tmpStr) tmpStr = "00";
		
		example		=  example.replace(regExp, tmpStr);
	}

	regExp.compile('mm');
	if (regExp.test(dateFormat)) {
	
		tmpStr      =  str.substring(10, 12)	
		if(!tmpStr) tmpStr = "00";
		
		example		=  example.replace(regExp, tmpStr);
	}

	regExp.compile('ss');
	if (regExp.test(dateFormat)) {
	
		tmpStr      =  str.substring(12, 14)	
		if(!tmpStr) tmpStr = "00";
		
		example		=  example.replace(regExp, tmpStr);
	}

	return example;
}

/*===================================================================================
 * 화면 중앙에 사이즈를 변경할 수 있는 팝업창 생성
 *
 * @param	sURL     : 팝업창 URL 경로
 			sParam   : 팝업창으로 넘겨줄 파라미터
 			sWinName : 팝업 윈도우의 이름
 			Width    : 팝업창의 넓이
			Height   : 팝업창의 높이
=================================================================================== */
function resizablePopup(sURL, sParam, sWinName, iWidth, iHeight) {

    var iLeft = (screen.width  - iWidth )/2;
	var iTop  = (screen.height - iHeight)/2;

	features = "width="+ iWidth + ",height=" + iHeight + ",left=" + iLeft + ",top=" + iTop +",scrollbars=yes,directories=no,menubar=no,resizable=yes";
	sURL = sURL + sParam;

	win = window.open(sURL, sWinName, features);

	return false;
}

/*==============================================================================
 * 날짜에 월을 더한다.
 *
 * @param	년, 월, 일, 계산할 월 (년도는 반드시 4자리로 입력)
 * @return	일자
/*==============================================================================*/
function addMonth(yyyy, mm, dd, pMonth) {

	var cDate; 						// 계산에 사용할 날짜 객체 선언
	var oDate; 						// 리턴할 날짜 객체 선언
	var cYear, cMonth, cDay; 		// 계산된 날짜값이 할당될 변수
	mm = mm*1 + ((pMonth*1)-1); 	// 월은 0~11 이므로 하나 빼준다
	cDate = new Date(yyyy, mm, dd);	// 계산된 날짜 객체 생성 (객체에서 자동 계산)
	cYear = cDate.getFullYear(); 	// 계산된 년도 할당
	cMonth = cDate.getMonth(); 		// 계산된 월 할당
	cDay = cDate.getDate(); 		// 계산된 일자 할당
	oDate = (dd == cDay) ? cDate : new Date(cYear, cMonth, 0); // 넘어간 월의 첫쨋날 에서 하루를 뺀 날짜 객체를 생성한다.
	return oDate;
}

/*===================================================================================
 * 날짜변환 (yyyy.mm.dd를 자바스크립트 Date로 변환
 *
 * @param	String형 일자
 * @return	DATE형 일자
/*==============================================================================*/
function shToDate(dateStr){

	if(dateStr.length == 10 && dateStr.indexOf(".") != -1 && dateStr.split(".").length == 3){
		var arrDate = dateStr.split(".");
		var rtnDate = new Date(arrDate[0], (parseInt(arrDate[1].replace(/^0/, ""))-1), arrDate[2]);

		return rtnDate;
	}
	return "";
}

function shToDate8(dateStr){


	if(dateStr.length == 8){
		
		var rtnDate = new Date(dateStr.substring(0,4), Number(dateStr.substring(4,6)) -1, dateStr.substring(6,8));
		return rtnDate;
	}
	return "";
}


/********************************************************************************
 *
 * 콤보박스의 selectedindex 의 Text 값을 리턴
 * arg1: arg2; 콤보박스 명 
 *
 *******************************************************************************/
function getCBtext( s_name){

	var tmp_si = s_name.selectedIndex;	
	
	var r_data = "";

	if(tmp_si == -1){
		r_data = "";
	}else{
		r_data = s_name.options[tmp_si].text;
	}
	return r_data;
}


/********************************************************************************
 *
 * 모달창의 크기 변경
 * arg1: arg2; 넓이 ,높이
 *
 *******************************************************************************/
function dialogReSize(dwidth, dheight){

    var winl = (screen.width - dwidth)/2;
    var wint = (screen.height- dheight)/2;

	if(window.dialogWidth != dwidth + "px" || window.dialogHeight != dheight + "px")
	{
		window.dialogLeft = winl+ "px";
		window.dialogTop = wint+ "px";
		window.dialogWidth= dwidth + "px";
		window.dialogHeight= dheight + "px";
	}
}
/**
 * @type   : function
 * @access : public
 * @desc   : DAFrame 결과 조회 (Summary)
 * <pre>
 * </pre>
 * @sig    : xmlObj,tagName
 * @param  : xmlObj - 부모 노드
 * @param  : tagName - 조회하고자하는 엘리먼트 이름
 * @return : 엘리먼트 텍스트
 * @author : 공통
 */
function cfnGetSvcSummary(xmlObj,tagName) {
    var ret = "";
    var rootXmlNode = Ext.DomQuery.selectNode("/EffectDTO/Summary", xmlObj);
    try {
        var childXmlNodes = rootXmlNode.childNodes;       
        Ext.each(childXmlNodes, function(childXmlNode) {
            if(childXmlNode.tagName == tagName) {
                ret = childXmlNode.firstChild.nodeValue;
            }
        });
    } catch(e) {
        alert(e);
    }
    return ret;
}


/**
 * @type   : function
 * @access : public
 * @desc   : 화면 가운데에 원하는 크기의 popup 창을 띠운다. (ExtJS type)
 * @sig    : url,modal,width,height,title
 * @param  : url - popup 창에 나타내고자 하는 url
 * @param  : modal - modal:true, modeless:false
 * @param  : width - popup 창의 width
 * @param  : height - popup 창의 height
 * @param  : title - 창타이틀바제목
 * @return : 없음
 * @author : 공통
 */
function cfnShowPopupWindow(url,modal,width,height,title) {
    if (cfnIsNull(title)) { title = APP_NAME; }
    var src = "<iframe id='popup' name='popup' src='" + url + "' onload='test(popup)' scrolling='no' frameborder='0' width='100%' height='100%'></iframe>";
    POPUP = new Ext.Window({
        width:width,
        height:height,
        title:title,
        autoScroll:true,
        modal:modal,
        html:src
    });
    POPUP.show();
}

/**
 * @type   : function
 * @access : public
 * @desc   : 팝업윈도우를 닫는다. (ExtJS type)
 * @return : 없음
 * @author : 공통
 */
function cfnClosePopup() {
    POPUP.hide();
}

/**
 * @type   : function
 * @access : public
 * @desc   : 화면 가운데에 원하는 크기의 popup 창을 띠운다.
 * @sig    : url, width, height
 * @param  : url - popup 창에 나타내고자 하는 url
 * @param  : width - popup 창의 width
 * @param  : height - popup 창의 height
 * @return : 없음
 * @author : 공통
 */
function cfnShowPopup(url, width, height, name) {
    var x = (screen.width - width) / 2;
    var y = (screen.height - height) / 2;

    if ( name == "" ) {
        name = "popup";
    }

    //var newWin = window.open(url, name,
    //  "width=" + width + ",height=" + height + ",left=" + x + ",top=" + y +
    //  ",resizable=no,status=no,scroll=no,toolbar=no,location=no,directories=no,menubar=no, scrollbars=no");
    //if ( parseInt(navigator.appVersion) >= 4) { newWin.window.focus(); }
    var winStyle = "width=" + width + ",height=" + height + ",left=" + x + ",top=" + y + ",resizable=no,status=no,scroll=no,toolbar=no,location=no,directories=no,menubar=no, scrollbars=no";
    var j = 0;
    var key = new Array();
    var value = new Array();
    var stIndex = url.indexOf("?");
    var newUrl = url;
    if (stIndex > 0) {
        newUrl = url.substring(0,stIndex);
        var keyvalue = url.substring(stIndex+1).split("&");
        for(i=0; i < keyvalue.length; i++) {
            var keypair = keyvalue[i].split('=');
            key[j] = keypair[0];
            value[j] = keypair[1];
            j++;  
        }
    }
    cfnOpenWindowWithPost(newUrl,name,winStyle,key,value);
}

function cfnShowPopupEx(url, width, height, name) {
    var x = (screen.width - width) / 2;
    var y = (screen.height - height) / 2;

    if ( name == "" ) {
        name = "popup";
    }

    var newWin = window.open(url, name,
      "width=" + width + ",height=" + height + ",left=" + x + ",top=" + y +
      ",resizable=no,status=no,scroll=no,toolbar=no,location=no,directories=no,menubar=no, scrollbars=no");
    if ( parseInt(navigator.appVersion) >= 4) { newWin.window.focus(); }
}

function cfnShowPopupFx(url, width, height, name, top, left) {

	if ( name == "" ) {
        name = "popup";
    }

    var newWin = window.open(url, name,
      "width=" + width + ",height=" + height + ",left=" + left + ",top=" + top +
      ",resizable=no,status=no,scroll=no,toolbar=no,location=no,directories=no,menubar=no, scrollbars=no");
    if ( parseInt(navigator.appVersion) >= 4) { newWin.window.focus(); }
}


/**
 * @type   : function
 * @access : public
 * @desc   : 화면 가운데에 스크롤바 있는 원하는 크기의 popup 창을 띠운다.
 * @sig    : url, width, height
 * @param  : url - popup 창에 나타내고자 하는 url
 * @param  : width - popup 창의 width
 * @param  : height - popup 창의 height
 * @return : 없음
 * @author : 공통
 */
function cfnShowScrollPopup(url, width, height, name) {
    var x = (screen.availWidth - width) / 2;
    var y = (screen.availHeight - height) / 2;

    if ( name == "" ) {
        name = "popup";
    }

    //var newWin = window.open(url, name,
    //  "width=" + width + ",height=" + height + ",left=" + x + ",top=" + y +
    //  ",resizable=no,status=no,scrollbars=yes,toolbar=no,location=no,directories=no,menubar=no");
    var winStyle = "width=" + width + ",height=" + height + ",left=" + x + ",top=" + y + ",resizable=no,status=no,scrollbars=yes,toolbar=no,location=no,directories=no,menubar=no";
    var j = 0;
    var key = new Array();
    var value = new Array();
    var stIndex = url.indexOf("?");
    var newUrl = url;
    if (stIndex > 0) {
        newUrl = url.substring(0,stIndex);
        var keyvalue = url.substring(stIndex+1).split("&");
        for(i=0; i < keyvalue.length; i++) {
            var keypair = keyvalue[i].split('=');
            key[j] = keypair[0];
            value[j] = keypair[1];
            j++;  
        }
    }
    cfnOpenWindowWithPost(newUrl,name,winStyle,key,value);
}

/**
 * @type   : function
 * @access : public
 * @desc   : 화면 가운데에 상태바, 스크롤바 있는 원하는 크기의 popup 창을 띠운다.
 * @sig    : url, width, height
 * @param  : url - popup 창에 나타내고자 하는 url
 * @param  : width - popup 창의 width
 * @param  : height - popup 창의 height
 * @return : 없음
 * @author : 공통
 */
function cfnShowStatusPopup(url, width, height, name) {
    var x = (screen.availWidth - width) / 2;
    var y = (screen.availHeight - height) / 2;

    if ( name == "" ) {
        name = "popup";
    }

    //var newWin = window.open(url, name,
    //  "width=" + width + ",height=" + height + ",left=" + x + ",top=" + y +
    //  ",resizable=no,status=yes,scrollbars=yes,toolbar=no,location=no,directories=no,menubar=no");
    var winStyle = "width=" + width + ",height=" + height + ",left=" + x + ",top=" + y + ",resizable=no,status=yes,scrollbars=yes,toolbar=no,location=no,directories=no,menubar=no";
    var j = 0;
    var key = new Array();
    var value = new Array();
    var stIndex = url.indexOf("?");
    var newUrl = url;
    if (stIndex > 0) {
        newUrl = url.substring(0,stIndex);
        var keyvalue = url.substring(stIndex+1).split("&");
        for(i=0; i < keyvalue.length; i++) {
            var keypair = keyvalue[i].split('=');
            key[j] = keypair[0];
            value[j] = keypair[1];
            j++;  
        }
    }
    cfnOpenWindowWithPost(newUrl,name,winStyle,key,value);
}

/**
 * @type   : function
 * @access : public
 * @desc   : 모달다이얼로그창을 띠운다.
 * <pre>
 *      Usage : cfnPopupModal('/popup.jsp?x=1',100,100);
 * </pre>
 * @sig    : url, width, height
 * @param  : url - 모달창에 나타내고자 하는 url(IE에서만 동작)
 * @param  : width - 모달창의 width
 * @param  : height - 모달창의 height
 * @return : 없음
 * @author : 공통
 */
function cfnPopupModal(url, width, height) {
    var strWinStyle = "dialogWidth=" + width + "px; dialogHeight=" + height + "px; center:yes; help:no; scroll:no; status:no;";
    return window.showModalDialog(url, window, strWinStyle);
}


function cfnPopupModalStyle(url, strWinStyle) {
    return window.showModalDialog(url, "pop", strWinStyle);
}
/**
 * @type   : function
 * @access : public
 * @desc   : 아규먼트 값을 가지는 모달다이얼로그창을 띠운다.(IE에서만 동작)
 * <pre>
 *      Usage : cfnPopupMdal('/popup.jsp?x=1', ["aaa","bbb"], 100, 100);
 * </pre>
 * @sig    : url, arg, width, height
 * @param  : url - 모달창에 나타내고자 하는 url
 * @param  : arg - 모달에서 받을 데이터
 * @param  : width - 모달창의 width
 * @param  : height - 모달창의 height
 * @return : 없음
 * @author : 공통
 */
function cfnPopupModalArg(url, arg, width, height) {
    strWinStyle = "dialogWidth=" + width + "px; dialogHeight=" + height + "px; center:yes; status=no; help:no; scroll:no ";
    return window.showModalDialog(url, arg, strWinStyle);

}

/**
 * @type   : function
 * @access : public
 * @desc   : 메세지박스
 * <pre>
 *      Usage : cfnAlert('메세지','타이틀','info','ok');
 *              cfnAlert('메세지','','','yesno','funYesNo'); 
 * </pre>
 * @sig    : msg,title,icon,button,callback
 * @param  : msg - 표시할 메세지
 * @param  : title - Alert창의 타이틀 메세지 (Optional) 
 * @param  : icon - 좌측에 표시될 아이콘 (Optional)
 *              error : 에러아이콘
 *              info : 정보아이콘 ==> default
 *              warning : 경고아이콘
 *              question : 질문아이콘
 * @param  : button - 버튼종류지정 (Optional)
 *              ok - OK 버튼 ==> default
 *              cancel - CANCEL 버튼
 *              okcancel - OK,CANCEL 버튼
 *              yesno -YES,NO 버튼
 *              yesnocancel -YES,NO,CANCEL 버튼
 * @param  : callback - 콜백함수명 (Optional)             
 * @return : 없음
 * @author : 공통
 */
function cfnAlert(msg,title,icon,button,callback) {
    if (cfnIsNull(title)) { title = APP_NAME; }
    if (cfnIsNull(icon)) { icon = 'ext-mb-info'; }
    if (icon == 'error') { icon = 'ext-mb-error'; }
    if (icon == 'info') { icon = 'ext-mb-info'; }
    if (icon == 'question') { icon = 'ext-mb-question'; }
    if (icon == 'warning') { icon = 'ext-mb-warning'; }
    if (cfnIsNull(button)) { buttons = Ext.MessageBox.OK; }
    if (button == 'ok') { buttons = Ext.MessageBox.OK; }
    if (button == 'cancel') { buttons = Ext.MessageBox.CANCEL; }
    if (button == 'okcancel') { buttons = Ext.MessageBox.OKCANCEL; }
    if (button == 'yesno') { buttons = Ext.MessageBox.YESNO; }
    if (button == 'yesnocancel') { buttons = Ext.MessageBox.YESNOCANCEL; }
    
    Ext.MessageBox.show({
        title: title,
        msg: msg,
        buttons: buttons,
        fn: eval(callback),
        width:250,
        icon: icon
    });
}


function cfnIsNull(val){
	
	if(val == "" || val == null){
		return true;
	}else{
		return false;
	}
	
}
 
/**
 * @type   : function
 * @access : public
 * @desc   : 입력 메세지박스
 * <pre>
 *      Usage : cfnAlertConfirm('메세지','callback','타이틀');
 *              cfnAlertConfirm('메세지','callback'); => 기본타이틀
 * </pre>
 * @sig    : msg,title,callback,multiline,width,value
 * @param  : msg - 표시할 메세지
 * @param  : title - Alert창의 타이틀 메세지 (Optional) 
 * @param  : callback - 메세지창의 버튼을 클릭시 실행 할 함수명 (Optional)
 * @param  : multiline - 여러줄의 입력창 표시 여부 (Optional)
 *              yes - 멀티라인 표시
 *              no - 한줄표시
 * @param  : width - 메세지박스 넓이 (Optional)
 * @param  : value - 입력창에 전달할 문자열값 (Optional) 
 * @return : 없음
 * @author : 공통
 */
function cfnAlertPrompt(msg,title,callback,multiline,width,value) {
    
    if (cfnIsNull(title)) { title = APP_NAME; }
    if (multiline == 'yes') {
        Ext.MessageBox.show({
            title: title,
            msg: msg,
            width: width,
            buttons: Ext.MessageBox.OKCANCEL,
            multiline: true,
            fn: eval(callback),
            value: value
      });
    } else {
        var scope;
        multiline = false;
        Ext.MessageBox.prompt(title, msg, eval(callback),scope,multiline,value);
    }
}


/**
 * @type   : function
 * @access : public
 * @desc   : DAFrame 결과 조회 (Master)
 * <pre>
 * </pre>
 * @sig    : xmlObj,tagName
 * @param  : xmlObj - 부모 노드
 * @param  : tagName - 조회하고자하는 엘리먼트 이름
 * @return : 엘리먼트 텍스트
 * @author : 공통
 */
function cfnGetSvcMaster(xmlObj,tagName) {
    var ret = "";
    var rootXmlNode = Ext.DomQuery.selectNode("/EffectDTO/Master", xmlObj);
    try {
        var childXmlNodes = rootXmlNode.childNodes;
        Ext.each(childXmlNodes, function(childXmlNode) {
            if(childXmlNode.tagName == tagName) {
                ret = childXmlNode.firstChild.nodeValue;
            }
        });
    } catch(e) {
    	
    }
    return ret;       
}
/**
 * @type   : function
 * @access : public
 * @desc   : DAFrame 결과 조회 (Message)
 * <pre>
 * </pre>
 * @sig    : xmlObj,tagName
 * @param  : xmlObj - 부모 노드
 * @param  : tagName - 조회하고자하는 엘리먼트 이름
 * @return : 엘리먼트 텍스트
 * @author : 공통
 */
function cfnGetSvcMessage(xmlObj,tagName) {
    var ret = "";
    var rootXmlNode = Ext.DomQuery.selectNode("/EffectDTO/Summary", xmlObj);
    try {
        var childXmlNodes = rootXmlNode.childNodes;
        Ext.each(childXmlNodes, function(childXmlNode) {
            if(childXmlNode.tagName == tagName) {
                ret = childXmlNode.firstChild.nodeValue;
            }
        });
    } catch(e) {
    	
    }
    return ret;       
}

/**
 * @type   : function
 * @access : public
 * @desc   : DAFrame 결과 조회 (RESULT)
 * <pre>
 * </pre>
 * @sig    : xmlObj,tagName
 * @param  : xmlObj - 부모 노드
 * @param  : tagName - 조회하고자하는 엘리먼트 이름
 * @return : 엘리먼트 텍스트
 * @java	: putMTO 사용시 저장정보를 가져옴.
 * @author : 공통
 */
function cfnGetSvcMtoList(xmlObj,tagName) {
    var ret = "";
    var rootXmlNode = Ext.DomQuery.selectNode("/EffectDTO/MtoList/RESULT", xmlObj);
    try {
        var childXmlNodes = rootXmlNode.childNodes;
        Ext.each(childXmlNodes, function(childXmlNode) {
            if(childXmlNode.tagName == tagName) {
                ret = childXmlNode.firstChild.nodeValue;
            }
        });
    } catch(e) {
    	
    }
    return ret;       
}



/**
 * @type   : function
 * @access : public
 * @desc   : DAFrame 전송대기 프로그래스바
 * <pre>
 * </pre>
 */
function funWaitWindow(mode, msg) {
    
    if (mode == true) {
        Ext.MessageBox.show({
            msg: msg,
            progressText: '',
            width:200,
            wait:true,
            waitConfig: {interval:200}
            
        });
    } else {
        Ext.MessageBox.hide();
    }             
}	
/**
 * @type   : extjs
 * @access : public
 * @desc   : Store에서 파라미터로 만들기
 * @chkCol   : 체크박스
 * <pre>
 * </pre>
 */
function storeToparam(store, chkCol) {
	
    var param = new Object();
    param['LTO_NAME'] = "INPUT_GRID";
    param['LTO_HEADER'] = new Array();
    
    
    
    var cnt = 0;
    var dateType = typeof(new Date());
	for(var ii=0; ii < store.length; ii++)
	{
		var record = store[ii]['data'];
		
		if(chkCol) {
		
			if(record[chkCol])
			{
				for(cell in record)
				{
				
					if(cnt == 0) {
						param[cell] = new Array();
						param['LTO_HEADER'][param['LTO_HEADER'].length] = cell;
					}
					
					
					if(typeof(record[cell]) == dateType )
					{
						param[cell][param[cell].length] = record[cell].format('Ymd');
					}else{
						param[cell][param[cell].length] = record[cell];
					}
					
				}
				
				cnt ++;
			}
		}else{			
				
			for(cell in record)
			{
				if(cnt == 0) {
					param[cell] = new Array();
					param['LTO_HEADER'][param['LTO_HEADER'].length] = cell;
				}
				
				
				if(typeof(record[cell]) == dateType )
				{
					param[cell][param[cell].length] = record[cell].format('Ymd');
				}else{
					param[cell][param[cell].length] = record[cell];
				}
			}
			
			cnt ++;
		}
		
		
	}  
	return param;
}	
/**
 * @type   : extjs
 * @access : public
 * @desc   : Store에서 파라미터 vType 체크
 * <pre>
 * </pre>
 */
function storevTypeChk(store) {
	
	for(var ii=0; ii < store.length; ii++)
	{
		var record = store[ii]['data'];			
			
		for(cell in record)
		{
			if(!record[cell].validate()) {
				return false;
			}
		}
	}  
	return true;
}	

function successGrid( result, request, fucName, msgFuc) {
   	var response = result.responseXML;
   	var Message = cfnGetSvcMessage(response,"Message");
   	var Code = cfnGetSvcMessage(response,"Code");
   	var Success = cfnGetSvcMessage(response,"Success");
   	funWaitWindow(false);
   	
   	if(Success == "true") {
   		
   		Ext.MessageBox.alert('Status', Message, msgFuc);
   		if(fucName) {
   			eval(fucName).call(this);
   		}
   		return true;
   	} else{
   		Ext.MessageBox.alert('Status', Message);
   		return false;
   	}
	
}

// MessageBox.confirm
function messageBoxConfirm(msg, fucName){
	Ext.MessageBox.confirm('Confirm', msg, showResult);

	function showResult(btn){
		if(btn == "yes") eval(fucName).call(this)
		else return;
	};
}

function renderNumber(v) {
    v = String(v);
    var ps = v.split('.');
    var whole = ps[0];
    var sub = ps[1] ? '.'+ ps[1] : '';
    var r = /(\d+)(\d{3})/;
    while (r.test(whole)) {
        whole = whole.replace(r, '$1' + ',' + '$2');
    }
    v = whole + sub;
    if(v.charAt(0) == '-'){
        return '-' + v.substr(1);
    }
    return v;
}

//===============================
// 파일다운로드 
//===============================
function fileDownFnc(fileUploadId, seqNo){

	if(!document.getElementById("fileDownTarget"))
	{  
		var ifr = document.createElement("iframe");
		ifr.setAttribute("name","fileDownTarget") ;
		ifr.setAttribute("id","fileDownTarget") ;
		ifr.style.display = "none";
		
		document.body.appendChild(ifr);
	}
	
	var ifr = document.getElementById("fileDownTarget")
	//var url = "FCCMA01.X01.cmd?FILEUPLOAD_ID=" + fileUploadId + "&SEQ_NO=" + seqNo;
	
	ifr.src = url;	
}

//===============================
// FAB 구분 확인
//===============================
function rtnFabNM(val){
	if(val == '2000'){
		return '부천';
	}else if(val == '3000'){
		return '상우';
	}else{
		return '본사';
	}

}


//===============================
// 요일 구분 
//===============================

function rtnDaysNum(sDate){

	var holiday = '|20100101|20100215|20100301|20100505|20100521|20100602|20100921|20100922|20100923|20110202|20110203|20110204|20110301|20110505|20110510|20110606|20110815|20110912|20110913|20111003|20120123|20120124';
	var yy = parseInt(sDate.substr(0, 4), 10);
    var mm = parseInt(sDate.substr(5, 2), 10);
    var dd = parseInt(sDate.substr(8), 10);
	var yyyymmdd = sDate.substr(0, 4)+sDate.substr(5,2)+sDate.substr(8);
	
	var weekday = new Array(7);	
    var d = new Date(yy,mm - 1, dd);

    if(holiday.indexOf(yyyymmdd) >0){
		return '0';
	}
	//var weekday = new Array("일", "월", "화", "수", "목", "금", "토");

    weekday[0]='0';
    weekday[1]='1';
    weekday[2]='2';
    weekday[3]='3';
    weekday[4]='4';
    weekday[5]='5';
    weekday[6]='6';

    return weekday[d.getDay()];
	
	
}
