function makeCodeSelectors(psetOrgArr) {
  var deferreds = [];
  if ($.isArray(psetOrgArr)) {
    for (var i = 0, n = psetOrgArr.length; i < n; i++) {
      deferreds[i] = makeCodeSelector(psetOrgArr[i]);
    }
  } else {
    $("body").append("<div>파라미터는 array 형식이어야 합니다.</div>");
  }

  return deferreds;
}

function makeCodeSelector(psetOrg) {
  /**
pset : preset. 콤보를 만들기 위해 필요한 기본값 셋.
속성 -
-. fieldId : [필수] 콤보와 결합할 input 필드 id(유일한 값이어야 함).
-. codeId :[필수] 콤보를 생성할 코드 목록의 이름.
-. type : [필수] 콤보 타입. list/radio 지원
-. width : 콤보의 너비. px 단위. 지정하지 않으면 목록 길이에 따라 너비가 변경됨.
-. initVal : 콤보의 초기값. 지정하지 않으면 목록의 첫번째 값이 선택됨.
-. selectAll : 콤보의 첫번째 목록으로 '전체' 또는 '선택'을 사용할 경우 해당되는 문구. 지정하지 않으면 전체 선택 옵션 없음. (값은 '')
-. isHide : 콤보를 화면에서 숨길 경우 true. 기본값은 false.
-. excludeCodes : 화면에 보이는 코드 목록 중 제외할 코드의 목록을 array 형태로 받음. 형식은 ['제외할 코드 1', '제외할 코드 2', ...].
-. callbackFunc : 콤보에서 값을 선택할 경우 발생하는 이벤트(callback). (파라미터 없음)
*/
  var defaults = {
    type: "list",
    width: "auto",
    isHide: false,
    excludeCodes: [],
    url: "/gcms/NCMA01.R01.cmd",
    setFocus: false,
    callbackFunc: function () {},
  };

  var pset = $.extend({}, defaults, psetOrg);
  var errorMsg = "";
  var deferred;

  if (pset.hasOwnProperty("fieldId") && pset.hasOwnProperty("codeId")) {
    $("#" + pset.fieldId).css("visibility", "hidden");
    $("#" + pset.fieldId).hide();
    switch (pset.type) {
      case "list":
        deferred = makeListCombo(pset);
        break;
      case "radio":
        deferred = makeRadioCombo(pset);
        break;
      default:
        errorMsg = "정의되지 않은 selector type입니다.";
        break;
    }
  } else {
    errorMsg = "input Field 또는 code ID가 지정되지 않았습니다.";
  }

  if (errorMsg.length > 0) {
    $("#" + pset.fieldId).after("<div>" + errorMsg + "</div>");
  }

  return deferred;
}

function makeSelectors(psetOrgArr) {
  if ($.isArray(psetOrgArr)) {
    for (var i = 0, n = psetOrgArr.length; i < n; i++) {
      makeSelector(psetOrgArr[i]);
    }
  } else {
    $("body").append("<div>파라미터는 배열 양식이어야 합니다.</div>");
  }
}

function makeSelector(psetOrg) {
  /**
pset : preset. 콤보를 만들기 위해 필요한 기본값 셋.
속성 -
-. fieldId : [필수] 콤보와 결합할 input 필드 id(유일한 값이어야 함)
-. type : [필수] 콤보 타입. list/radio
-. width : 콤보의 너비. px 단위. 지정하지 않으면 목록 길이에 따라 너비가 변경됨.
-. initVal : 콤보의 초기값. 지정하지 않으면 목록의 첫번째 값이 선택됨. (text가 아닌 value를 지정해야 함)
-. isHide : 콤보를 화면에서 숨길 경우 true. 기본값은 false.
-. selectAll : 콤보의 첫번째 목록으로 '전체' 또는 '선택'을 사용할 경우 나타나는 문구. 지정하지 않으면 전체 선택 옵션 없음. (값은 '')
-. codeData : 콤보에 사용할 데이터 array 또는 데이터 array를 생성하는 함수. 형식은 [{value: '코드', text:'코드 텍스트'}, {value: '코드2', text:'코드 텍스트2'}...]
*/
  var defaults = {
    type: "list",
    width: "auto",
    isHide: false,
    setFocus: false,
    callbackFunc: function () {},
  };

  var pset = $.extend({}, defaults, psetOrg);
  var errorMsg = "";

  if (pset.hasOwnProperty("fieldId") && pset.hasOwnProperty("codeData")) {
    // codeData가 함수 형식으로 왔다면 실행.
    if ($.isFunction(pset.codeData) == true) {
      pset.codeData = pset.codeData();
    }
    $("#" + pset.fieldId).css("visibility", "hidden");
    $("#" + pset.fieldId).hide();
    switch (pset.type) {
      case "list":
        makeListComboHtml(pset, pset.codeData);
        break;
      case "radio":
        makeRadioComboHtml(pset, pset.codeData);
        break;
      default:
        errorMsg = "정의되지 않은 selector type입니다.";
        break;
    }
  } else {
    errorMsg =
      "input Field가 지정되지 않았거나 코드 목록으로 사용할 데이터가 없습니다.";
  }

  if (errorMsg.length > 0) {
    $("body").after("<div>" + errorMsg + "</div>");
  }
}

function makeCustomSelectors(psetOrgArr) {
  var deferreds = [];
  if ($.isArray(psetOrgArr)) {
    for (var i = 0, n = psetOrgArr.length; i < n; i++) {
      deferreds[i] = makeCustomSelector(psetOrgArr[i]);
    }
  } else {
    $("body").append("<div>파라미터는 array 형식이어야 합니다.</div>");
  }

  return deferreds;
}

function makeCustomSelector(psetOrg) {
  /**
pset : preset. 콤보를 만들기 위해 필요한 기본값 셋.
속성 -
-. fieldId : [필수] 콤보와 결합할 input 필드 id(유일한 값이어야 함).
-. type : [필수] 콤보 타입. list/radio 지원
-. width : 콤보의 너비. px 단위. 지정하지 않으면 목록 길이에 따라 너비가 변경됨.
-. initVal : 콤보의 초기값. 지정하지 않으면 목록의 첫번째 값이 선택됨.
-. selectAll : 콤보의 첫번째 목록으로 '전체' 또는 '선택'을 사용할 경우 해당되는 문구. 지정하지 않으면 전체 선택 옵션 없음. (값은 '')
-. isHide : 콤보를 화면에서 숨길 경우 true. 기본값은 false.
-. url : 기존 코드 이외의 custom 코드를 DB에서 불러올 경우 AJAX URL. (값은 항상 VALUE, TEXT 형태여야 한다.)
-. callbackFunc : 콤보에서 값을 선택할 경우 발생하는 이벤트(callback). (파라미터 없음)
*/
  var defaults = {
    type: "list",
    width: "auto",
    isHide: false,
    setFocus: false,
    callbackFunc: function () {},
  };

  var pset = $.extend({}, defaults, psetOrg);
  var deferred;
  var errorMsg = "";

  if (pset.hasOwnProperty("fieldId") && pset.hasOwnProperty("url")) {
    $("#" + pset.fieldId).css("visibility", "hidden");
    $("#" + pset.fieldId).hide();
    switch (pset.type) {
      case "list":
        deferred = makeCustomListCombo(pset);
        break;
      case "radio":
        deferred = makeCustomRadioCombo(pset);
        break;
      default:
        errorMsg = "정의되지 않은 selector type입니다.";
        break;
    }
  } else {
    errorMsg = "input Field 또는 AJAX URL이 지정되지 않았습니다.";
  }

  if (errorMsg.length > 0) {
    $("body").after("<div>" + errorMsg + "</div>");
  }

  return deferred;
}

function makeListCombo(pset) {
  var ajaxObj = fetchCodeDataAndMakeCombo(pset, makeListComboHtml);
  return ajaxObj;
}

function makeCustomListCombo(pset) {
  var ajaxObj = fetchCustomDataAndMakeCombo(pset, makeListComboHtml);
  return ajaxObj;
}

function makeListComboHtml(pset, codeData) {
  /**
-. width : 콤보의 너비. px 단위. 지정하지 않으면 목록 길이에 따라 너비가 변경됨.
-. initVal : 콤보의 초기값. 지정하지 않으면 목록의 첫번째 값이 선택됨.
-. selectAll : 콤보의 첫번째 목록으로 '전체' 또는 '선택'을 사용할 경우 해당되는 문구. 지정하지 않으면 전체 선택 옵션 없음. (값은 '')
*/
  var codeHtml =
    '<div style="display:inline" id="divSelector' +
    pset.fieldId +
    '"><select id="listFor' +
    pset.fieldId +
    '" name="listFor' +
    pset.fieldId +
    '" class="search_form_select" style="height:20px;width:' +
    pset.width +
    '">';

  if (pset.hasOwnProperty("selectAll")) {
    codeHtml = codeHtml + '<option value="">' + pset.selectAll + "</option>";
  }

  if (pset.hasOwnProperty("initVal")) {
    for (var i = 0, n = codeData.length; i < n; i++) {
      codeHtml =
        codeHtml +
        '<option value="' +
        codeData[i].value +
        '"' +
        (codeData[i].value == pset.initVal ? " selected" : "") +
        ">" +
        codeData[i].text +
        "</option>";
    }
  } else {
    for (var i = 0, n = codeData.length; i < n; i++) {
      codeHtml =
        codeHtml +
        '<option value="' +
        codeData[i].value +
        '">' +
        codeData[i].text +
        "</option>";
    }
  }

  codeHtml = codeHtml + "</select></div>";

  $("#" + pset.fieldId).before(codeHtml);
  $("#listFor" + pset.fieldId)
    .change(function () {
      $("#" + pset.fieldId).val(
        $("#listFor" + pset.fieldId + " option:selected").val()
      );
      pset.callbackFunc();
      $("#" + pset.fieldId).focusout();
    })
    .change();

  if (pset.setFocus == true) {
    $("#listFor" + pset.fieldId).focus();
  }

  if (pset.isHide == true) {
    $("#divSelector" + pset.fieldId).hide();
  }
}

function makeRadioCombo(pset) {
  var ajaxObj = fetchCodeDataAndMakeCombo(pset, makeRadioComboHtml);
  return ajaxObj;
}

function makeCustomRadioCombo(pset) {
  var ajaxObj = fetchCustomDataAndMakeCombo(pset, makeRadioComboHtml);
  return ajaxObj;
}

function makeRadioComboHtml(pset, codeData) {
  /**
-. width : 콤보의 너비. px 단위. 지정하지 않으면 목록 길이에 따라 너비가 변경됨.
-. initVal : 콤보의 초기값. 지정하지 않으면 목록의 첫번째 값이 선택됨.
-. selectAll : 콤보의 첫번째 목록으로 '전체' 또는 '선택'을 사용할 경우 해당되는 문구. 지정하지 않으면 전체 선택 옵션 없음. (값은 '')
*/
  var codeHtml =
    '<div style="display:inline;width:' +
    pset.width +
    '" id="divSelector' +
    pset.fieldId +
    '">';

  if (pset.hasOwnProperty("selectAll")) {
    codeHtml =
      codeHtml +
      '<input type="radio" name="radioFor' +
      pset.fieldId +
      '" value="" /><label> ' +
      pset.selectAll +
      "</label>&nbsp;&nbsp;";
  }

  if (pset.hasOwnProperty("initVal")) {
    for (var i = 0, n = codeData.length; i < n; i++) {
      codeHtml =
        codeHtml +
        '<input type="radio" name="radioFor' +
        pset.fieldId +
        '" id="radioFor' +
        pset.fieldId +
        "_" +
        codeData[i].value +
        '" value="' +
        codeData[i].value +
        '"' +
        (codeData[i].value == pset.initVal ? " checked" : "") +
        ' /><label for="radioFor' +
        pset.fieldId +
        "_" +
        codeData[i].value +
        '"> ' +
        codeData[i].text +
        "</label>&nbsp;&nbsp;";
    }
  } else {
    for (var i = 0, n = codeData.length; i < n; i++) {
      codeHtml =
        codeHtml +
        '<input type="radio" name="radioFor' +
        pset.fieldId +
        '" id="radioFor' +
        pset.fieldId +
        "_" +
        codeData[i].value +
        '" value="' +
        codeData[i].value +
        '" /><label for="radioFor' +
        pset.fieldId +
        "_" +
        codeData[i].value +
        '"> ' +
        codeData[i].text +
        "</label>&nbsp;&nbsp;";
    }
  }

  codeHtml = codeHtml + "</div>";

  $("#" + pset.fieldId).before(codeHtml);

  $("input[name=radioFor" + pset.fieldId + "]")
    .change(function () {
      $("#" + pset.fieldId).val(
        $("input[name=radioFor" + pset.fieldId + "]:checked").val()
      );
      pset.callbackFunc();
      $("#" + pset.fieldId).focusout();
    })
    .change();

  if (pset.setFocus == true) {
    $("input[name=radioFor" + pset.fieldId + "]").focus();
  }

  if (pset.isHide == true) {
    $("#divSelector" + pset.fieldId).hide();
  }
}

function fetchCodeDataAndMakeCombo(pset, cbFunc) {
  var ajaxCode = $.ajax({
    url: pset.url,
    type: "GET",
    data: { CODEBASE: pset.codeId, EXCLUDECODES: pset.excludeCodes },
    dataType: "xml",
  });

  $.when(ajaxCode).done(function (res) {
    saveCodeDataAndMakeCombo(pset, res, cbFunc);
  });

  return ajaxCode;
}

function fetchCustomDataAndMakeCombo(pset, cbFunc) {
  var ajaxCode = $.ajax({
    url: pset.url,
    type: "GET",
    dataType: "xml",
  });

  $.when(ajaxCode).done(function (res) {
    saveCustomDataAndMakeCombo(pset, res, cbFunc);
  });

  return ajaxCode;
}

function saveCodeDataAndMakeCombo(pset, codeData, cbFunc) {
  cbFunc(pset, convertXmlToObject(codeData, "CODE", "CODE_NAME"));
}

function saveCustomDataAndMakeCombo(pset, data, cbFunc) {
  cbFunc(pset, convertXmlToObject(data, "VALUE", "TEXT"));
}

function convertXmlToObject(data, keyName, valueName) {
  var dataArr = [];

  $(data)
    .find("Row")
    .each(function (i) {
      var dataObj = {};
      dataObj.value = $(this).find(keyName).text();
      dataObj.text = $(this).find(valueName).text();
      dataArr.push(dataObj);
    });

  return dataArr;
}

// 달력 세팅
/**
pset : 달력 설정에 필요한 기본값 셋

format : 날짜 형식. 정의하지 않으면 "Y-m-d"으로 설정된다.
initVal : 날짜 초기값. 정의하지 않거나 ""(공백)이면 당일 날짜를 사용하고, "yyyy-mm-dd" 형식의 값을 정의하면 그 날짜를 초기값으로 사용한다.
width : 필드의 폭. 정의하지 않으면 100px이다.
fieldId : [필수] 달력이 매핑될 필드의 id. 정의하지 않으면 오류 메시지가 나타난다.
-- 날짜는 연도, 월, 일의 순으로 적용됨을 유의.
addDate : 날짜의 초기값에 더하거나(양수) 뺄(음수) 일 수
addMonth : 날짜의 초기값에 더하거나(양수) 뺄(음수) 개월 수
addYear : 날짜의 초기값에 더하거나(양수) 뺄(음수) 연도 수
*/
function makeDatePickers(psetOrgArr) {
  if ($.isArray(psetOrgArr)) {
    for (var i = 0, n = psetOrgArr.length; i < n; i++) {
      makeDatePicker(psetOrgArr[i]);
    }
  } else {
    $("body").append("<div>파라미터는 array 형식이어야 합니다.</div>");
  }
}

function makeDatePicker(psetOrg) {
  var defaults = {
    format: "Y-m-d",
    initVal: "",
    width: "80",
    setBlank: false,
    addDate: 0,
    addMonth: 0,
    addYear: 0,
    maxDate: "9999-12-31",
    minDate: "1800-01-01",
    minDateRefFieldId: "",
    maxDateRefFieldId: "",
  };
  var pset = $.extend({}, defaults, psetOrg);

  pset.initVal = getInitValueDate(pset);

  if (pset.hasOwnProperty("fieldId")) {
    var dateObj = new Ext.form.DateField({
      id: pset.fieldId + "dateObj",
      format: pset.format,
      applyTo: pset.fieldId,
      value: pset.initVal,
      width: pset.width,
      maxValue: pset.maxDate,
      minValue: pset.minDate,
      style: publicStyle,
    });

    $("#" + pset.fieldId).css("width", pset.width);

    // can't use readonly : all key event are blocked but only tab is allowed
    $("#" + pset.fieldId).focus(function (event) {
      //$("#"+pset.fieldId).focusout();
      return false;
    });
    $("#" + pset.fieldId).mousedown(function (event) {
      return false;
    });
    $("#" + pset.fieldId).keypress(function (event) {
      if (event.keyCode != "9") {
        return false;
      } else {
        return true;
      }
    });
    $("#" + pset.fieldId).keydown(function (event) {
      if (event.keyCode != "9") {
        return false;
      } else {
        return true;
      }
    });
    $("#" + pset.fieldId).keyup(function (event) {
      if (event.keyCode != "9") {
        return false;
      } else {
        return true;
      }
    });

    if (pset.minDateRefFieldId != "") {
      $("#" + pset.minDateRefFieldId).focusout(function () {
        var mindate = Ext.getCmp(pset.minDateRefFieldId + "dateObj").getValue();
        if (mindate == "") {
          mindate = pset.minDate;
        }

        var extDateObj = Ext.getCmp(pset.fieldId + "dateObj");
        extDateObj.setMinValue(mindate);
      });
    } else if (pset.maxDateRefFieldId != "") {
      $("#" + pset.maxDateRefFieldId).focusout(function () {
        var maxdate = Ext.getCmp(pset.maxDateRefFieldId + "dateObj").getValue();
        if (maxdate == "") {
          maxdate = pset.maxDate;
        }

        var extDateObj = Ext.getCmp(pset.fieldId + "dateObj");
        extDateObj.setMaxValue(maxdate);
      });
    }

    $("#" + pset.fieldId).val(pset.initVal);
    $("#" + pset.fieldId).focusout();
  } else {
    $("body").append("<div>달력을 적용할 text field가 없습니다.</div>");
  }
}

function getInitValueDate(pset) {
  var initDateStr;

  if (pset.setBlank == false) {
    var initDate;

    if (pset.initVal == "") {
      initDate = new Date();
    } else {
      var splitDate = pset.initVal.split("-");
      initDate = new Date(
        splitDate[0] * 1,
        splitDate[1] * 1 - 1,
        splitDate[2] * 1
      );
      if (isNaN(initDate.getTime())) {
        initDate = new Date();
      }
    }

    if (pset.addYear != 0) {
      initDate.setYear(1 * pset.addYear + initDate.getFullYear());
    }
    if (pset.addMonth != 0) {
      initDate.setMonth(1 * pset.addMonth + initDate.getMonth());
    }
    if (pset.addDate != 0) {
      initDate.setDate(1 * pset.addDate + initDate.getDate());
    }

    initDateStr = [
      initDate.getFullYear(),
      initDate.getMonth() < 9
        ? "0" + (initDate.getMonth() + 1)
        : "" + (initDate.getMonth() + 1),
      initDate.getDate() < 10
        ? "0" + initDate.getDate()
        : "" + initDate.getDate(),
    ].join("-");
  } else {
    initDateStr = "";
  }

  return initDateStr;
}

// 검색 팝업
/**
pset : 검색 팝업 설정에 필요한 기본값 셋
fieldId :[필수] 검색 팝업이 적용될 필드 ID.
fieldWidth : 검색 팝업이 적용될 필드 너비. 기본값은 100px.
title : [필수] 검색 팝업의 제목.
type : 검색 팝업의 속성. 현재는 'modal'로 고정(정의할 필요 없음. 차후 확장을 위한 필드.)
width : 검색 팝업 가로 크기. 기본값은 500px
height : 검색 팝업 세로 크기. 기본값은 400px
useIcon : 돋보기 아이콘 사용 여부. 기본값은 true.
hideField : 검색용 필드 감춤 여부. 기본값은 false.
sName : [필수] 검색 대상 셋의 속성명. 미리 정해진 값을 사용해야 함.
--> 임직원(재직자) 검색 : "임직원(재직자)"
--> 임직원(퇴직자) 검색 : "임직원(퇴직자)"
--> 외국인(회원) 검색 : "외국인(회원)"
--> 내국인(회원) 검색 : "내국인(회원)"
--> 결재자 검색 : "결재자"
--> 부서 검색 : "부서"
--> 업체 검색 : "업체"
callbackFunc : 팝업 창이 닫혔을 때 실행할 callback 함수. 인자는 1개(팝업창에서 리턴된 object).
*/
function makeSearchPopup(psetOrg) {
  var defaults = {
    fieldWidth: "100",
    title: "title",
    type: "modal",
    initVal: {},
    disableSearchField: false,
    width: "500",
    height: "400",
    useIcon: true,
    hideField: false,
    callbackFunc: function () {},
  };
  var pset = $.extend({}, defaults, psetOrg);

  if (pset.hasOwnProperty("fieldId") && pset.hasOwnProperty("sName")) {
    switch (pset.type) {
      case "modal":
        var agent = navigator.userAgent.toLowerCase();
        if (
          (navigator.appName == "Netscape" &&
            navigator.userAgent.search("Trident") != -1) ||
          agent.indexOf("msie") != -1
        ) {
          // IE
          makeModalSearchPopup(pset);
        } else {
          // non-IE (not accept modal)
          makeModelessSearchPopup(pset);
        }
        break;
      case "modeless":
        makeModelessSearchPopup(pset);
        break;
      default:
        makeModelessSearchPopup(pset);
        break;
    }
  } else {
    errorMsg = "검색 팝업 필드 또는 검색 대상이 정의되지 않았습니다.";
    $("body").append("<div>" + errorMsg + "</div>");
  }
}

/**
pset : 검색 팝업 설정에 필요한 기본값 셋
--> 이 함수는 img 태그의 onclick 이벤트에 매핑된다.

title : [필수] 검색 팝업의 제목.
type : 검색 팝업의 속성. 현재는 'modal'로 고정(정의할 필요 없음. 차후 확장을 위한 필드.)
width : 검색 팝업 가로 크기. 기본값은 500px
height : 검색 팝업 세로 크기. 기본값은 400px
sName : [필수] 검색 대상 셋의 속성명. 미리 정해진 값을 사용해야 함.
--> 임직원(재직자) 검색 : "임직원(재직자)"
--> 임직원(퇴직자) 검색 : "임직원(퇴직자)"
--> 외국인(회원) 검색 : "외국인(회원)"
--> 내국인(회원) 검색 : "내국인(회원)"
--> 결재자 검색 : "결재자"
--> 부서 검색 : "부서"
--> 업체 검색 : "업체"
callbackFunc : 팝업 창이 닫혔을 때 실행할 callback 함수. 인자는 1개(팝업창에서 리턴된 object).
*/
function makeSearchButton(psetOrg) {
  var defaults = {
    fieldWidth: "100",
    title: "title",
    type: "modal",
    width: "500",
    height: "400",
    callbackFunc: function () {},
  };
  var pset = $.extend({}, defaults, psetOrg);

  if (pset.hasOwnProperty("sName")) {
    switch (pset.type) {
      case "modal":
        var agent = navigator.userAgent.toLowerCase();
        if (
          (navigator.appName == "Netscape" &&
            navigator.userAgent.search("Trident") != -1) ||
          agent.indexOf("msie") != -1
        ) {
          // IE
          makeModalSearchButton(pset);
        } else {
          // non-IE (not accept modal)
          makeModelessSearchButton(pset);
        }
        break;
      case "modeless":
        makeModelessSearchButton(pset);
        break;
      default:
        makeModelessSearchButton(pset);
        break;
    }
  } else {
    $("body").append("<div>검색 대상이 설정되지 않았습니다.</div>");
  }
}

function makeModalSearchButton(pset) {
  var searchSetName = getSearchSetName(pset.sName);
  var sObj = window.showModalDialog(
    searchSetName +
      "&search=&mod=modal&title=" +
      encodeURIComponent(pset.title),
    "POPUP",
    "dialogWidth:" +
      pset.width +
      "px;dialogHeight:" +
      pset.height +
      "px;status:no;scroll:no;"
  );
  pset.callbackFunc(sObj);
}

function makeModalSearchPopup(pset) {
  var searchSetName = getSearchSetName(pset.sName);

  $("#" + pset.fieldId).before(
    "<input type='text' id='sTextFor" +
      pset.fieldId +
      "' class='search_form' style='width:" +
      pset.fieldWidth +
      "px;'>"
  );
  $("#" + pset.fieldId).css("visibility", "hidden");
  $("#" + pset.fieldId).hide();

  if (pset.hideField == true) {
    $("#sTextFor" + pset.fieldId).hide();
  }

  if (pset.disableSearchField == true) {
    $("#sTextFor" + pset.fieldId).attr("disabled", "disabled");
  }

  if ($.isEmptyObject(pset.initVal) == false) {
    $("#sTextFor" + pset.fieldId).val(pset.initVal.searchText);
    pset.callbackFunc(pset.initVal);
  }

  var searchActionFunc = function () {
    var searchOrgValue = tagStripper($("#sTextFor" + pset.fieldId).val());
    var searchValue = encodeURIComponent(searchOrgValue);
    if (pset.fieldId == "NOTEBOOK_MANAGEMENT_NO") {
      if ($("#V_MEMBER_ID").text() == "") {
        alert("사원 정보를 먼저 조회하세요.");
        return;
      }
      searchValue = $("#V_MEMBER_ID").text();
    }
    var sObj = window.showModalDialog(
      searchSetName +
        "&search=" +
        searchValue +
        "&title=" +
        encodeURIComponent(pset.title) +
        "&mod=modal",
      "POPUP",
      "dialogWidth:" +
        pset.width +
        "px;dialogHeight:" +
        pset.height +
        "px;status:no;scroll:no;"
    );

    $("#sTextFor" + pset.fieldId).val(searchOrgValue);
    if (sObj != undefined && sObj != null) {
      pset.callbackFunc(sObj);
      $("#sTextFor" + pset.fieldId).val($("#" + pset.fieldId).val());
    }
  };

  $("#sTextFor" + pset.fieldId).change(function (event) {
    searchActionFunc();
  });

  if (pset.useIcon == true) {
    $("#" + pset.fieldId).after(
      "&nbsp;<img id='cImgFor" +
        pset.fieldId +
        "' src='/gcms/images/main/work/icon_src.gif' style='cursor:pointer;vertical-align: top; margin-top: 3px'>"
    );

    // 돋보기 이미지에 이벤트 붙임
    $("#cImgFor" + pset.fieldId).click(function () {
      searchActionFunc();
    });

    // 검색 필드에 이벤트 붙임
    $("#sTextFor" + pset.fieldId).keypress(function (event) {
      if (event.keyCode == 13) {
        // Enter
        searchActionFunc();
      }
    });
  } else {
    // 검색 필드에 이벤트 붙임
    $("#sTextFor" + pset.fieldId).keypress(function (event) {
      if (event.keyCode == 13) {
        // Enter
        searchActionFunc();
      }
    });
  }
}

function makeModelessSearchButton(pset) {
  var searchSetName = getSearchSetName(pset.sName);
  var cName = /function ([^\(]*)/.exec(pset.callbackFunc.toString())[1];
  var uName = "none";
  var sObj = makeCallbackPopup({
    width: pset.width,
    height: pset.height,
    uName: uName,
    cName: cName,
    callbackFunc: pset.callbackFunc,
    unloadFunc: function () {},
    url:
      searchSetName +
      "&search=&title=" +
      encodeURIComponent(pset.title) +
      "&mod=modeless&cName=" +
      cName +
      "&uName=" +
      uName,
  });
}

function makeModelessSearchPopup(pset) {
  var searchSetName = getSearchSetName(pset.sName);

  $("#" + pset.fieldId).before(
    "<input type='text' id='sTextFor" +
      pset.fieldId +
      "' class='search_form' style='width:" +
      pset.fieldWidth +
      "px;'>"
  );
  $("#" + pset.fieldId).css("visibility", "hidden");
  $("#" + pset.fieldId).hide();

  if (pset.hideField == true) {
    $("#sTextFor" + pset.fieldId).hide();
  }

  if ($.isEmptyObject(pset.initVal) == false) {
    $("#sTextFor" + pset.fieldId).val(pset.initVal.searchText);
    pset.callbackFunc(pset.initVal);
  }

  var searchActionFunc = function () {
    var searchOrgValue = tagStripper($("#sTextFor" + pset.fieldId).val());
    var searchValue = encodeURIComponent(searchOrgValue);

    var cName = "C_" + pset.fieldId;
    var uName = "U_" + pset.fieldId;
    var sObj = makeCallbackPopup({
      width: pset.width,
      height: pset.height,
      uName: uName,
      cName: cName,
      callbackFunc: pset.callbackFunc,
      unloadFunc: function () {
        if ($("#" + pset.fieldId).val() == "") {
          $("#sTextFor" + pset.fieldId).val(searchOrgValue);
        } else {
          $("#sTextFor" + pset.fieldId).val($("#" + pset.fieldId).val());
        }
      },
      url:
        searchSetName +
        "&search=" +
        searchValue +
        "&title=" +
        encodeURIComponent(pset.title) +
        "&mod=modeless&cName=" +
        cName +
        "&uName=" +
        uName,
    });
  };

  $("#sTextFor" + pset.fieldId).change(function (event) {
    searchActionFunc();
  });

  if (pset.useIcon == true) {
    $("#" + pset.fieldId).after(
      "&nbsp;<img id='cImgFor" +
        pset.fieldId +
        "' src='/gcms/images/main/work/icon_src.gif' style='cursor:pointer;vertical-align: top; margin-top: 3px'>"
    );

    // 돋보기 이미지에 이벤트 붙임
    $("#cImgFor" + pset.fieldId).click(function () {
      searchActionFunc();
    });

    // 검색 필드에 이벤트 붙임
    $("#sTextFor" + pset.fieldId).keypress(function (event) {
      if (event.keyCode == 13) {
        // Enter
        searchActionFunc();
      }
    });
  } else {
    // 검색 필드에 이벤트 붙임
    $("#sTextFor" + pset.fieldId).keypress(function (event) {
      if (event.keyCode == 13) {
        // Enter
        searchActionFunc();
      }
    });
  }
}

function makeCallbackPopup(popupSettingOrg) {
  var defaults = {
    width: 750,
    height: 580,
    scroll: "no",
    toolbar: "no",
    resizable: "no",
    menubar: "no",
    callbackFunc: function () {},
    unloadFunc: function () {},
    url: "error.html",
  };

  var popupSetting = $.extend({}, defaults, popupSettingOrg);

  var left =
    popupSetting.hasOwnProperty("left") == false
      ? (window.screen.width - popupSetting.width) / 2
      : popupSetting.left;
  var top =
    popupSetting.hasOwnProperty("top") == false
      ? (window.screen.height - popupSetting.height) / 2
      : popupSetting.top;
  var status =
    "location=no, toolbar=" +
    popupSetting.toolbar +
    ",directories=no,resizable=" +
    popupSetting.resizable +
    ",status=no,menubar=" +
    popupSetting.menubar +
    ",width=" +
    popupSetting.width +
    ",height=" +
    popupSetting.height +
    ", top=" +
    top +
    ",left=" +
    left +
    ",scrollbars=" +
    popupSetting.scroll;
  var title = "t_" + new Date().getTime();

  var windowName = "P_" + popupSetting.cName;
  if (windowName in window) {
    delete window[popupSetting.cName];
    delete window[popupSetting.uName];
    window[windowName].close();
  }

  if (!(popupSetting.cName in window)) {
    window[popupSetting.cName] = popupSetting.callbackFunc;
  }

  if (!(popupSetting.uName in window)) {
    window[popupSetting.uName] = popupSetting.unloadFunc;
  }

  window[windowName] = window.open(popupSetting.url, title, status, true);
  return window[windowName];
}

function tagStripper(value) {
  var tagStripRegex = /<\/?[^>]+(>|$)/g;
  var specialCharRegex = /[\|&;=\*\?\$%@"'<>\(\)\+,]/g;
  var sqlRegex =
    / *insert *| *update *| *delete *| *select *| *create *| *where *| *set *| *from */gi;

  var strippedStr = $.trim(value).replace(tagStripRegex, "");
  strippedStr = strippedStr.replace(specialCharRegex, "");
  strippedStr = strippedStr.replace(sqlRegex, "");

  return strippedStr;
}

function getSearchSetName(sName) {
  var searchSetName;
  switch (sName) {
    case "부서":
      searchSetName = "NCMA10.PL01.cmd?option=";
      break;
    case "업체":
      searchSetName = "NCMA11.PL01.cmd?option=" + encodeURIComponent("업체");
      break;
    case "업체(등록)":
      searchSetName =
        "NCMA11.PL01.cmd?option=" + encodeURIComponent("업체(등록)");
      break;
    case "자재":
      searchSetName = "NCMA12.PL01.cmd?option=";
      break;
    case "결재자":
      searchSetName = "NCMA13.PL01.cmd?option=";
      break;
    case "임직원(재직자)":
      searchSetName =
        "NCMA14.PL01.cmd?option=" + encodeURIComponent("임직원(재직자)");
      break;
    case "임직원(퇴직자)":
      searchSetName =
        "NCMA14.PL01.cmd?option=" + encodeURIComponent("임직원(퇴직자)");
      break;
    case "동일업체방문자":
      searchSetName =
        "NCMA14.PL01.cmd?option=" + encodeURIComponent("동일업체방문자");
      break;
    case "방문자":
      searchSetName = "NCMA14.PL01.cmd?option=" + encodeURIComponent("방문자");
      break;
    case "통문관리(전체인원)":
      searchSetName =
        "NCMA14.PL01.cmd?option=" + encodeURIComponent("통문관리(전체인원)");
      break;
    case "인수자":
      searchSetName = "NCMA14.PL01.cmd?option=" + encodeURIComponent("인수자");
      break;
    case "PR NO":
      searchSetName = "NCMA15.PL01.cmd?option=";
      break;
    case "우편번호":
      searchSetName = "NCMA16.PL01.cmd?option=";
      break;
    case "구매팀(Fab1)":
      searchSetName =
        "NCMA14.PL01.cmd?option=" + encodeURIComponent("구매팀(Fab1)");
      break;
    case "구매팀(Fab2)":
      searchSetName =
        "NCMA14.PL01.cmd?option=" + encodeURIComponent("구매팀(Fab2)");
      break;
    case "NB" /* 노트북 21.04.14 추가 */:
      searchSetName = "NCMA22.PL01.cmd?option=sub";
      break;
    default:
      break;
  }

  return searchSetName;
}

/**
셀렉터의 선택값을 프로그램적으로 변경한다.
*/
function setSelectorValue(fieldId, value) {
  var selectorStr = "#divSelector" + fieldId + " > select:last-child";
  var selectorId = $(selectorStr).attr("name");
  if (selectorId == undefined) {
    selectorStr = "#divSelector" + fieldId + " > input:first-child";
    selectorId = $(selectorStr).attr("name");
  }

  var selectorType = selectorId.split("For")[0];
  switch (selectorType) {
    case "list":
      setListSelectorValue(selectorId, value);
      break;
    case "radio":
      setRadioSelectorValue(selectorId, value);
      break;
    default:
      break;
  }
}

function setListSelectorValue(selectorId, value) {
  $("#" + selectorId).val(value);
  $("#" + selectorId).change();
}

function setRadioSelectorValue(selectorId, value) {
  $("input[name=" + selectorId + "][value=" + value + "]").attr(
    "checked",
    "checked"
  );
  $("input[name=" + selectorId + "][value=" + value + "]").change();
}

/**
셀렉터의 선택된 값에 대한 텍스트를 가져온다.
*/
function getSelectorText(fieldId) {
  var selectorStr = "#divSelector" + fieldId + " > select:last-child";
  var selectorId = $(selectorStr).attr("name");
  if (selectorId == undefined) {
    selectorStr = "#divSelector" + fieldId + " > input:first-child";
    selectorId = $(selectorStr).attr("name");
  }

  var selectorType = selectorId.split("For")[0];

  var selectedText = "";
  switch (selectorType) {
    case "list":
      selectedText = getListSelectorText(selectorId);
      break;
    case "radio":
      selectedText = getRadioSelectorText(selectorId);
      break;
    default:
      break;
  }

  return selectedText;
}

function getListSelectorText(selectorId) {
  return $("#" + selectorId + " option:selected").text();
}

function getRadioSelectorText(selectorId) {
  return $("input[name=" + selectorId + "]:checked + label").text();
}

/**
셀렉터를 화면에 보이게 한다.
*/
function showSelector(fieldId) {
  var divID = "#divSelector" + fieldId;
  $(divID).show();
}

/**
셀렉터를 화면에 보이지 않게 한다.
*/
function hideSelector(fieldId) {
  var divID = "#divSelector" + fieldId;
  $(divID).hide();
}

function makeSearchFields(psetOrgArr) {
  if ($.isArray(psetOrgArr)) {
    for (var i = 0, n = psetOrgArr.length; i < n; i++) {
      makeSearchField(psetOrgArr[i]);
    }
  } else {
    $("body").append("<div>파라미터는 array 형식이어야 합니다.</div>");
  }
}

function makeSearchField(psetOrg) {
  var defaults = {
    width: "auto",
    initVal: "",
    useHangul: true,
    placeHolder: "",
    maxlength: "25",
    setFocus: false,
  };

  var pset = $.extend({}, defaults, psetOrg);

  if (pset.hasOwnProperty("fieldId")) {
    makeFieldForSearch(pset);
  } else {
    $("body").after("<div>input Field가 지정되지 않았습니다.</div>");
  }
}

function makeFieldForSearch(pset) {
  var fieldIdSharp = "#" + pset.fieldId;
  $(fieldIdSharp).addClass("search_form");
  $(fieldIdSharp).val(pset.initVal);
  $(fieldIdSharp).prop({
    maxlength: pset.maxlength,
    placeholder: pset.placeHolder,
  });
  $(fieldIdSharp).css({ width: pset.width });
  if (pset.useHangul == true) {
    $(fieldIdSharp).css({ "ime-mode": "active" });
  } else {
    $(fieldIdSharp).css({ "ime-mode": "inactive" });
  }

  $(fieldIdSharp).change(function () {
    $(fieldIdSharp).val(tagStripper($(fieldIdSharp).val()));
  });

  if (pset.setFocus == true) {
    $(fieldIdSharp).focus();
  }
}

// GRID COMBOBOX 생성용
function makeExtJsCombo(applyTo, emptyText, codeBase) {
  if (!emptyText) emptyText = "전체";

  // 레코드 생성
  var myStore = new Ext.data.Store({
    url: "/gcms/NCMA01.R01.cmd",
    reader: new Ext.data.XmlReader(
      {
        record: "Row",
      },
      Ext.data.Record.create(["CODE", "CODE_NAME"])
    ),
  });

  myStore.load({
    params: { CODEBASE: codeBase },
  });

  var disCom = new Ext.form.ComboBox({
    store: myStore,
    displayField: "CODE_NAME",
    valueField: "CODE",
    triggerAction: "all",
    lazyRender: true,
    mode: "local",
    emptyText: emptyText,
    style: publicStyle,
    applyTo: applyTo,
    editable: false,
  });

  // 기본 데이터 추가분
  if (emptyText == "전체") {
    var defaultData = {
      CODE: "",
      CODE_NAME: "전체",
    };

    var rec_Id = 100; // // provide unique id  IE9에서 0으로 설정시 안나옴
    var r = new myStore.recordType(defaultData, rec_Id);

    myStore.on("load", function (handler, scope) {
      myStore.insert(0, r);
    });
  }
  // 기본 데이터 추가분 끝

  disCom.on("blur", function (field) {
    if (applyTo) {
      if (
        Ext.get(applyTo).getValue() == emptyText ||
        Ext.get(applyTo).getValue() == "" ||
        !disCom.selectByValue(disCom.getValue(), false)
      ) {
        field.setValue("");
      }
    }
  });

  disCom.setWidth(150);
  disCom.setReadOnly();
  return disCom;
}

function clearSearchPopupSearchField(fieldId) {
  $("#sTextFor" + fieldId).val("");
}
