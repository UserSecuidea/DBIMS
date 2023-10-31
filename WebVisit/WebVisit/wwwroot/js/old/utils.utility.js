// 텍스트에서 특수문자를 제거하는 함수
function textStripper(value) {
  //var tagStripRegex = /<\/?[^>]+(>|$)/g;
  var specialCharRegex = /[\|&;=\*\?\$%@"'<>\(\)\+,]/g;
  //var sqlRegex = / *insert *| *update *| *delete *| *select *| *create *| *where *| *set *| *from */gi;

  //var strippedStr = ($.trim(value)).replace(tagStripRegex, '');
  //strippedStr = strippedStr.replace(specialCharRegex, '');
  //strippedStr = strippedStr.replace(sqlRegex, '');

  var strippedStr = $.trim(value).replace(specialCharRegex, "");

  return strippedStr;
}

// 팝업을 만들어 주는 함수
/**
width : 팝업의 가로 폭. 지정하지 않을 경우 기본값은 750px.
height : 팝업의 세로 폭. 지정하지 않을 경우 기본값은 580px.
top : 팝업의 위치 중 높이. 지정하지 않으면 기본값은 화면 중앙.
top : 팝업의 위치 중 왼쪽 위치. 지정하지 않으면 기본값은 화면 중앙.
url : [필수] 팝업으로 띄울 페이지의 URL.
scroll : 스크롤바 여부. 지정하지 않을 경우 기본값은 'no'
toolbar : 툴바 보이기 여부. 지정하지 않을 경우 기본값은 'no'
resizable : 화면 크기 변경 가능 여부. 지정하지 않을 경우 기본값은 'no'
menubar : 메뉴바 보이기 여부. 지정하지 않을 경우 기본값은 'no'
*/
function makePopup(popupSettingOrg) {
  var defaults = {
    width: 750,
    height: 580,
    left: null,
    top: null,
    scroll: "no",
    toolbar: "no",
    resizable: "no",
    menubar: "no",
    url: "error.html",
  };

  var popupSetting = $.extend({}, defaults, popupSettingOrg);

  var left =
    popupSetting.left == null
      ? (window.screen.width - popupSetting.width) / 2
      : popupSetting.left;
  var top =
    popupSetting.top == null
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

  if (window.childWin) {
    window.childWin.close();
  }
  window.childWin = window.open(popupSetting.url, title, status, true);
}

function makePresetTextFields(psetOrgArr) {
  if ($.isArray(psetOrgArr)) {
    for (var i = 0, n = psetOrgArr.length; i < n; i++) {
      makePresetTextField(psetOrgArr[i]);
    }
  } else {
    $("body").append("<div>파라미터는 array 형식이어야 합니다.</div>");
  }
}

function makePresetTextField(psetOrg) {
  var defaults = {
    width: "auto",
    useHangul: true,
    placeHolder: "",
    setFocus: false,
  };

  var pset = $.extend({}, defaults, psetOrg);

  if (pset.hasOwnProperty("fieldId")) {
    makeFieldForText(pset);
  } else {
    $("body").after("<div>input Field가 지정되지 않았습니다.</div>");
  }
}

function makeFieldForText(pset) {
  var fieldIdSharp = "#" + pset.fieldId;
  $(fieldIdSharp).addClass("search_form");
  $(fieldIdSharp).prop({ placeholder: pset.placeHolder });
  $(fieldIdSharp).css({ width: pset.width });
  if (pset.hasOwnProperty("height")) {
    $(fieldIdSharp).css({ height: pset.height });
  }
  if (pset.useHangul == true) {
    $(fieldIdSharp).css({ "ime-mode": "active" });
  }

  //$(fieldIdSharp).change(function(){
  //	$(fieldIdSharp).val(textStripper($(fieldIdSharp).val()));
  //});

  if (pset.setFocus == true) {
    $(fieldIdSharp).focus();
  }
}
