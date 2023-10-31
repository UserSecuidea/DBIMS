// <CONOZ>
var PREV_URL = null;

function deepCopyObject(inObject) {
  var outObject, value, key;
  if (typeof inObject !== "object" || inObject === null) {
    return inObject;
  }
  outObject = Array.isArray(inObject) ? [] : {};
  for (key in inObject) {
    value = inObject[key];
    outObject[key] =
      typeof value === "object" && value !== null
        ? deepCopyObject(value)
        : value;
  }
  return outObject;
}

function setBizNo(str, id) {
  str = str.replace(/[^0-9]/g, "");
  let tmp = "";
  if (str.length < 4) {
    $("#" + id).val(str);
  } else if (str.length < 6) {
    tmp += str.substr(0, 3);
    tmp += "-";
    tmp += str.substr(3);
    $("#" + id).val(tmp);
  } else if (str.length < 11) {
    tmp += str.substr(0, 3);
    tmp += "-";
    tmp += str.substr(3, 2);
    tmp += "-";
    tmp += str.substr(5);
    $("#" + id).val(tmp);
  }
}

function validateEmail(email) {
  let validRegex =
    /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
  // /^[a-z0-9\.\_%+-]+@@[a-z0-9\.\-]+\.[a-z]{2,4}$/i
  if (email.match(validRegex)) {
    return true;
  } else {
    return false;
  }
}

function isValidDate(d) {
  return d instanceof Date && !isNaN(d);
}

function ToYY_MM_DD(d) {
  let mm = d.getMonth() + 1; // getMonth() is zero-based
  let dd = d.getDate();

  return [
    d.getFullYear(),
    (mm > 9 ? "" : "0") + mm,
    (dd > 9 ? "" : "0") + dd,
  ].join("-");
}

function ToYYMMDD(d) {
  let mm = d.getMonth() + 1; // getMonth() is zero-based
  let dd = d.getDate();

  return [
    d.getFullYear(),
    (mm > 9 ? "" : "0") + mm,
    (dd > 9 ? "" : "0") + dd,
  ].join("");
}

function isNumber(x) {
  // console.log("[isNumber]" + x);
  if (Number.isNaN(x)) {
    return false;
  }
  if (isNaN(x)) {
    return false;
  }
  return true;
}

function generateDateString(obj) {
  with (obj) {
    // 입력된값중 포함된 문자있으면 backspace
    var a =
      "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz~`!#$%^&*()_+|}{\":?><=-\\][';/.,";
    for (i = 0; i < a.length; i++) {
      if (
        obj.value.substr(obj.value.length - 1, obj.value.length) == a.charAt(i)
      ) {
        obj.value = obj.value.substr(0, obj.value.length - 1);
      }
    }
  }
  var change, cnt;
  change = obj.value;
  cnt = change.length;
  var returnValue = false;
  if (cnt == 4) {
    obj.value = obj.value + "-";
  }
  if (cnt == 6) {
    if (obj.value.substr(5, 6) != "0" && obj.value.substr(5, 6) != "1") {
      obj.value = obj.value.substr(0, 5) + "0" + obj.value.substr(5, 6) + "-";
    }
  }
  if (cnt == 7) {
    obj.value = obj.value + "-";
  }
  if (cnt == 9) {
    if (
      obj.value.substr(8, 9) != "0" &&
      obj.value.substr(8, 9) != "1" &&
      obj.value.substr(8, 9) != "2" &&
      obj.value.substr(8, 9) != "3"
    ) {
      obj.value = obj.value.substr(0, 8) + "0" + obj.value.substr(8, 9);
    }
  }
  if (cnt == 10) {
    //날짜 유효성 검사
    var reg =
      /^(19|20)(\d){2}(\/|-|_)(0[1-9]|1[0-2])(\/|-|_)(0[1-9]|[1-2][0-9]|3[0-1])$/;
    if (reg.test(obj.value)) {
      returnValue = true;
    } else {
      console.log("[generateDateString] invalid date format");
      obj.value = "";
    }
    return returnValue;
  }
  if (event.keyCode == 8 && cnt == 9) {
    // 일자 -> -
    obj.value = obj.value.substr(0, obj.value.length - 2) + "-";
  } else if (event.keyCode == 8 && cnt == 7) {
    // delete month
    obj.value = obj.value.substr(0, obj.value.length - 3);
  } else if (event.keyCode == 8) {
    // delete year
    obj.value = "";
  }
}

function generatePhoneString(obj) {
  with (obj) {
    // 입력된값중 포함된 문자있으면 backspace
    var a =
      "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz~`!#$%^&*()_+|}{\":?><=-\\][';/.,";
    for (i = 0; i < a.length; i++) {
      if (
        obj.value.substr(obj.value.length - 1, obj.value.length) == a.charAt(i)
      ) {
        obj.value = obj.value.substr(0, obj.value.length - 1);
      }
    }
  }
  var change, cnt;
  change = obj.value;
  cnt = change.length;
  var returnValue = false;
  if (cnt == 3) {
    obj.value = obj.value + "-";
  }
  if (cnt == 8) {
    obj.value = obj.value + "-";
  }
  if (cnt == 13) {
    //휴대폰 번호 유효성 검사
    var reg = /^010-[0-9]{4}-[0-9]{4}$/;
    if (reg.test(obj.value)) {
      returnValue = true;
    } else {
      console.log("[generatePhoneString] invalid date format");
      obj.value = "";
    }
    return returnValue;
  }
  if (event.keyCode == 8 && cnt == 9) {
    // 일자 -> -
    obj.value = obj.value.substr(0, obj.value.length - 2) + "-";
  } else if (event.keyCode == 8 && cnt == 7) {
    // delete month
    obj.value = obj.value.substr(0, obj.value.length - 3);
  } else if (event.keyCode == 8) {
    // delete year
    obj.value = "";
  }
}

function generateBizRegNoString(obj) {
  with (obj) {
    // 입력된값중 포함된 문자있으면 backspace
    var a =
      "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz~`!#$%^&*()_+|}{\":?><=-\\][';/.,";
    for (i = 0; i < a.length; i++) {
      if (
        obj.value.substr(obj.value.length - 1, obj.value.length) == a.charAt(i)
      ) {
        obj.value = obj.value.substr(0, obj.value.length - 1);
      }
    }
  }
  var change, cnt;
  change = obj.value;
  cnt = change.length;
  var returnValue = false;
  if (cnt == 3) {
    obj.value = obj.value + "-";
  }
  if (cnt == 6) {
    obj.value = obj.value + "-";
  }
  if (cnt == 12) {
    //사업자 등록증 유효성 검사
    var reg = /^[0-9]{3}-[0-9]{2}-[0-9]{5}$/;
    if (reg.test(obj.value)) {
      returnValue = true;
    } else {
      console.log("[generateBizRegNoString] invalid date format");
      obj.value = "";
    }
    return returnValue;
  }
  if (event.keyCode == 8 && cnt == 9) {
    // 일자 -> -
    obj.value = obj.value.substr(0, obj.value.length - 2) + "-";
  } else if (event.keyCode == 8 && cnt == 7) {
    // delete month
    obj.value = obj.value.substr(0, obj.value.length - 3);
  } else if (event.keyCode == 8) {
    // delete year
    obj.value = "";
  }
}

/**
findIndex(myform, strFirstElementName, obj)
findFormElementByIndex(myform, strFirstElementName, strFindElementName, idx)
 * findIndex(myform, strFirstElementName, obj)
 * @param {*} myform
 * @param {*} strFirstElementName
 * @param {*} obj
 * @returns
 */
function findIndex(myform, strFirstElementName, obj) {
  let j = -1;
  // for (var i = 0; i < myform.length; i++) {
  //   var el = myform[i];
  for (const el of myform) {
    if (el.name == strFirstElementName) {
      j++;
    }
    if (el == obj) {
      console.log("[findIndex] idx: " + j);
      break;
    }
  }
  return j;
}
/**
 * findFormElementByIndex(myform, strFirstElementName, strFindElementName, idx)
 * @param {*} myform
 * @param {*} strFirstElementName
 * @param {*} strFindElementName
 * @param {*} idx
 * @returns
 */
function findFormElementByIndex(
  myform,
  strFirstElementName,
  strFindElementName,
  idx
) {
  let j = -1;
  // for (var i = 0; i < myform.length; i++) {
  //   var el = myform[i];
  for (const el of myform) {
    if (el.name == strFirstElementName) {
      j++;
    }
    if (j == idx) {
      if (el.name == strFindElementName) {
        return el;
      }
    } else if (j > idx) {
      return null;
    }
  }
  return null;
}
// </CONOZ>

// publisher
$(function () {
  $.datepicker.setDefaults({
    closeText: "닫기", // Display text for close link
    prevText: "이전 달", // Display text for previous month link
    nextText: "다음 달", // Display text for next month link
    currentText: "오늘", // Display text for current month link
    monthNames: [
      "1월",
      "2월",
      "3월",
      "4월",
      "5월",
      "6월",
      "7월",
      "8월",
      "9월",
      "10월",
      "11월",
      "12월",
    ], // Names of months for drop-down and formatting
    monthNamesShort: [
      "1월",
      "2월",
      "3월",
      "4월",
      "5월",
      "6월",
      "7월",
      "8월",
      "9월",
      "10월",
      "11월",
      "12월",
    ], // For formatting
    dayNames: [
      "일요일",
      "월요일",
      "화요일",
      "수요일",
      "목요일",
      "금요일",
      "토요일",
    ], // For formatting
    dayNamesShort: ["일", "월", "화", "수", "목", "금", "토"], // For formatting
    dayNamesMin: ["일", "월", "화", "수", "목", "금", "토"], // Column headings for days starting at Sunday
    weekHeader: "Wk", // Column header for week of the year
    dateFormat: "yy-mm-dd", // See format options on parseDate
    firstDay: 0, // The first day of the week, Sun = 0, Mon = 1, ...
    isRTL: false, // True if right-to-left language, false if left-to-right
    showMonthAfterYear: true, // True if the year select precedes month, false for month then year
    yearSuffix: "년", // Additional text to append to the year in the month headers
  });
  common.calendar();
  common.etcEvt();
});

// popup
var isOpenPopup = false;
var popupObj;
common = {
  calendar: function () {
    $("body").on("focus", ".datepicker", function (ev) {
      $(this).datepicker({
        dateFormat: "y/mm/dd",
      });
    });
  },
  etcEvt: function () {
    $(window).scroll(function () {
      var ck = $(this).scrollTop();
      if (ck > 0) {
        $("header").addClass("on");
      } else {
        $("header").removeClass("on");
      }
    });
    $("body").on("click", ".btn-total-nav", function (ev) {
      console.log("[body>total-nav]");
      var ck = $(this).hasClass("on");
      if (ck) {
        $(this).removeClass("on");
        $(".header-item").removeClass("on");
      } else {
        $(this).addClass("on");
        $(".header-item").addClass("on");
      }
    });

    // yk: input type file
    // $("body").on("click", ".btn-upload", function (ev) {
    //   console.log("[body>click .btn-upload]");
    //   $(this).prev(".hidden").trigger("click");
    // });
    // // yk: input type file
    // $("body").on("change", ".hidden", function (ev) {
    //   console.log("[body>change .hidden]");
    //   $(this)
    //     .parents(".form-item-wrap")
    //     .eq(0)
    //     .find(".value")
    //     .text($(this).val());
    // });

    $("body").on("click", "header .gnb-list>li>a", function (ev) {
      console.log("[body>header]");
      var ck = $(this).hasClass("on");
      if (ck) {
        $(this).removeClass("on");
      } else {
        $("header .gnb-list>li>a").removeClass("on");
        $(this).addClass("on");
      }
    });
    $("body").on("click", ".mob-toggle-wrap a", function (ev) {
      console.log("[body>mob-toggle]");
      var ck = $(".mob-toggle-wrap").hasClass("on");
      if (ck) {
        $(".mob-toggle-wrap").removeClass("on");
      } else {
        $(".mob-toggle-wrap").addClass("on");
      }
    });
    $("body").on(
      "click",
      ".dialog .pop-item-wrap .pop-header-wrap a",
      function () {
        common.popClose($(this).parents(".dialog").eq(0));
        // $(this).parents(".dialog").eq(0).removeClass("on");
        // setTimeout(function () {
        //   $(this).parents(".dialog").eq(0).removeClass("ing");
        // }, 500);
      }
    );
  },
  popOpen: function (o) {
    console.log("[popOpen]");
    $(o).addClass("ing");
    setTimeout(function () {
      $(o).addClass("on");
      isOpenPopup = true;
      popupObj = o;
    }, 200);
  },
  popClose: function (o) {
    console.log("[popClose]");
    isOpenPopup = false;
    popupObj = null;
    $(o).removeClass("on");
    setTimeout(function () {
      $(o).removeClass("ing");
    }, 500);
  },
  layerClose: function (event) {
    console.log("[layerClose]");
    var ele = event.target.className;
    if (ele.indexOf("dialog") != -1) {
      //$(".dialog").removeClass("on");
      $(".dialog").removeClass("on");
      setTimeout(function () {
        $(".dialog").removeClass("ing");
      }, 500);
    }
  },
  bussiness_num: function (str, id) {
    str = str.replace(/[^0-9]/g, "");
    let tmp = "";
    if (str.length < 4) {
      $("#" + id).val(str);
    } else if (str.length < 6) {
      tmp += str.substr(0, 3);
      tmp += "-";
      tmp += str.substr(3);
      $("#" + id).val(tmp);
    } else if (str.length < 11) {
      tmp += str.substr(0, 3);
      tmp += "-";
      tmp += str.substr(3, 2);
      tmp += "-";
      tmp += str.substr(5);
      $("#" + id).val(tmp);
    }
  },
  numberWithCommas: function (o) {
    var x = $(o).val();
    x = x.replace(/[^0-9]/g, ""); // 입력값이 숫자가 아니면 공백
    x = x.replace(/,/g, ""); // ,값 공백처리
    $(o).val(x.replace(/\B(?=(\d{3})+(?!\d))/g, ",")); // 정규식을 이용해서 3자리 마다 , 추가
  },
  inNumber: function (evt) {
    var code = evt.which ? evt.which : event.keyCode;
    if (code < 48 || code > 57) {
      return false;
    }
  },
  inNumber01: function (evt) {
    var code = evt.which ? evt.which : event.keyCode;
    if (code < 48 || code > 57) {
      if (code != 46) {
        alert("숫자만 입력가능합니다.");
        return false;
      }
    }
  },
  removeChar(event) {
    event = event || window.event;
    var keyID = event.which ? event.which : event.keyCode;
    if (keyID == 8 || keyID == 46 || keyID == 37 || keyID == 39) {
      return;
    } else {
      event.target.value = event.target.value.replace(/[^-\.0-9]/g, "");
    }
  },
  inHangl: function (evt) {
    var code = evt.which ? evt.which : event.keyCode;
    if (code < 12592 || code > 12687) {
      return false;
    }
  },
  inId: function (event) {
    var regExp = /[ㄱ-ㅎ|ㅏ-ㅣ|가-힣]/g;
    var ele = event.target;
    if (regExp.test(ele.value)) {
      ele.value = ele.value.replace(regExp, "");
    }
  },
  maxLengthCheck: function (object) {
    //console.log(object.value);
    //object.value = object.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');
    if (object.value.length > object.maxLength) {
      object.value = object.value.slice(0, object.maxLength);
    }
  },
  chkCharCode: function (event) {
    var regExp = /[^0-9a-zA-Z]/g;
    var ele = event.target;
    if (regExp.test(ele.value)) {
      ele.value = ele.value.replace(regExp, "");
    }
  },
};
function execDaumPostcode() {
  new daum.Postcode({
    oncomplete: function (data) {
      // 팝업에서 검색결과 항목을 클릭했을때 실행할 코드를 작성하는 부분.

      // 각 주소의 노출 규칙에 따라 주소를 조합한다.
      // 내려오는 변수가 값이 없는 경우엔 공백('')값을 가지므로, 이를 참고하여 분기 한다.
      var addr = ""; // 주소 변수
      var extraAddr = ""; // 참고항목 변수

      //사용자가 선택한 주소 타입에 따라 해당 주소 값을 가져온다.
      if (data.userSelectedType === "R") {
        // 사용자가 도로명 주소를 선택했을 경우
        addr = data.roadAddress;
      } else {
        // 사용자가 지번 주소를 선택했을 경우(J)
        addr = data.jibunAddress;
      }

      // 사용자가 선택한 주소가 도로명 타입일때 참고항목을 조합한다.
      if (data.userSelectedType === "R") {
        // 법정동명이 있을 경우 추가한다. (법정리는 제외)
        // 법정동의 경우 마지막 문자가 "동/로/가"로 끝난다.
        if (data.bname !== "" && /[동|로|가]$/g.test(data.bname)) {
          extraAddr += data.bname;
        }
        // 건물명이 있고, 공동주택일 경우 추가한다.
        if (data.buildingName !== "" && data.apartment === "Y") {
          extraAddr +=
            extraAddr !== "" ? ", " + data.buildingName : data.buildingName;
        }
        // 표시할 참고항목이 있을 경우, 괄호까지 추가한 최종 문자열을 만든다.
        if (extraAddr !== "") {
          extraAddr = " (" + extraAddr + ")";
        }
        // 조합된 참고항목을 해당 필드에 넣는다.
        document.getElementById("daum_extraAddress").value = extraAddr;
      } else {
        document.getElementById("daum_extraAddress").value = "";
      }

      // 우편번호와 주소 정보를 해당 필드에 넣는다.
      document.getElementById("daum_postcode").value = data.zonecode;
      document.getElementById("daum_address").value = addr;
      // 커서를 상세주소 필드로 이동한다.
      document.getElementById("daum_detailAddress").focus();
    },
  }).open();
}
