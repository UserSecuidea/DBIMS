var publicStyle = 'width:100px;';

var appName = navigator.appName;
var appVer 	= navigator.appVersion;
var ua 		= navigator.userAgent;

if(appName == "Microsoft Internet Explorer") {

	var real_appVer = parseInt(ua.substring(ua.indexOf("MSIE ") + 5));

	if(real_appVer == 8) {
		publicStyle = publicStyle + 'margin:0 0 0 0;';
	}else  {
		publicStyle = publicStyle + 'margin:0 0 0 0;';
	}
}

//===============================
//타임필드를 생성한다.
//===============================
function createTimeField(applyTo, minVal, maxVal, incVal){

	var txt = new Ext.form.TimeField({
		applyTo:applyTo,
	    //fieldLabel: 'Time',
		id: applyTo,
	    name: applyTo,
	    minValue: minVal,
	    maxValue: maxVal,
	    emptyText:"선택",
	    format: 'H:i',
	    style: publicStyle,
	    increment: incVal
	});
	return txt;
}

//===============================
// 텍스트필드를 생성한다.
//===============================
function createTextField(applyTo, value)
{
    var txt = new Ext.form.TextField({
           applyTo:applyTo,
           id:applyTo,
           name:applyTo,
           value:value,
           width: 150
    });
    return txt;
}
//===============================
// 숫자필드를 생성한다.
//===============================
function createNumberField(applyTo, value)
{
    var txt = new Ext.form.NumberField({
           applyTo:applyTo,
           id:applyTo,
           name:applyTo,
           value:value,
           width: 150,
           style:"text-align:right",
           baseChars : '0123456789,'
    });
    return txt;
}

//===============================
// 숫자필드를 생성한다.
//===============================
function createNumberCommaField(applyTo, value, allowDecimals)
{
	if(allowDecimals == undefined) allowDecimals = true;

    var txt = new Ext.form.NumberCommaField({
           applyTo:applyTo,
           id:applyTo,
           name:applyTo,
           value:value,
           width: 150,
           style:"text-align:right",
           baseChars : '0123456789,',
           allowDecimals:allowDecimals
    });
    return txt;
}
//===============================
// 텍스트Area 필드를 생성한다.
//===============================
function createTextAreaField(applyTo, value)
{
    var txt = new Ext.form.TextArea({
           applyTo:applyTo,
           id:applyTo,
           name:applyTo,
           value:value,
           width: 150
    });
    return txt;
}
//===============================
// 달력을 생성한다.
//===============================
function createDateField(applyTo, value)
{
 	/* 기간 FROM*/
    var cal = new Ext.form.DateField({
           format: 'Y-m-d',
           applyTo: applyTo,
           width: 100,
           value: value,
           style: publicStyle
    });

    return cal;
}

//===============================
// 월 달력을 생성한다.
//===============================
function createMonthField(applyTo, value)
{
    var cal = new Ext.form.MonthField({
           format: 'Y-m',
           applyTo: applyTo,
           width: 80,
           value: value,
           style: publicStyle
    });

    return cal;
}

//===============================
// FROM ~ TO 달력 체크용 VType
//===============================
Ext.apply(Ext.form.VTypes, {
	frtoDateFieldCheck : function(val, field) {

        var date = field.parseDate(val);

        if(!date) return;

        if (field.startDateField && (!this.dateRangeMax || (date.getTime() != this.dateRangeMax.getTime()))) {
            var start = Ext.getCmp(field.startDateField);
            start.setMaxValue(date);
            start.validate();
            this.dateRangeMax = date;
        } else if (field.endDateField && (!this.dateRangeMin || (date.getTime() != this.dateRangeMin.getTime()))) {
            var end = Ext.getCmp(field.endDateField);
            end.setMinValue(date);
            end.validate();
            this.dateRangeMin = date;
        }

        return true;
	}
});

//===============================
// FROM ~ TO 달력 생성
//===============================
function createFromToDateField(fromApplyTo, toApplyTo, fromVal, toVal)
{
 	/* 기간 FROM*/
    var fromDate = new Ext.form.DateField({
           format: 'Y-m-d',
           applyTo: fromApplyTo,
           width: 100,
           name: 'startdt',
           id: 'startdt',
           endDateField: 'enddt',
           vtype: 'frtoDateFieldCheck',
           style: publicStyle
    });

    var toDate = new Ext.form.DateField({
    	   format: 'Y-m-d',
           applyTo: toApplyTo,
           width: 100,
           name: 'enddt',
           id: 'enddt',
           startDateField: 'startdt',
           vtype: 'frtoDateFieldCheck',
           style: publicStyle
    });

    fromDate.setValue(fromVal);
    toDate.setValue(toVal);

    var rtnObj = new Object();

    rtnObj["FROM"] = fromDate;
    rtnObj["TO"] = toDate;

    return rtnObj;
}

function createFromToDateField12(fromApplyTo, toApplyTo, fromVal, toVal)
{
 	/* 기간 FROM*/
    var fromDate = new Ext.form.DateField({
           format: 'Y-m-d',
           applyTo: fromApplyTo,
           width: 100,
           name: 'startdt',
           id: 'startdt',
           endDateField: 'enddt',
           vtype: 'frtoDateFieldCheck',
           style: publicStyle
    });

    var toDate = new Ext.form.DateField({
    	   format: 'Y-m-d',
           applyTo: toApplyTo,
           width: 100,
           name: 'enddt',
           id: 'enddt',
           startDateField: 'startdt',
           vtype: 'frtoDateFieldCheck',
           style: publicStyle
    });

    fromDate.setValue(fromVal);
    toDate.setValue(toVal);

    var rtnObj = new Object();

    rtnObj["FROM"] = fromDate;
    rtnObj["TO"] = toDate;

    return rtnObj;
}




//===============================
// 임의의 데이터로 콤보를 생성한다.
//===============================
function createCommonCombo(myData,applyTo , emptyText)
{
	if(!emptyText) emptyText = "선택";

	var rt = Ext.data.Record.create(['FLAG_ID', 'FLAG_NM']);
	var myStore = new Ext.data.Store({
		reader: new Ext.data.ArrayReader(
			{idIndex: 0}, rt
		)
	});

	myStore.loadData(myData);

	var disCom = new Ext.form.ComboBox({
		store: myStore,
		displayField:'FLAG_NM',
		valueField:'FLAG_ID',
		triggerAction: 'all',
		lazyRender:true,
		mode: 'local',
		emptyText: emptyText,
		style: publicStyle,
		applyTo: applyTo
	});

	disCom.on('blur', function(field) {
		if(applyTo) {
			if(Ext.get(applyTo).getValue() == emptyText || Ext.get(applyTo).getValue() == '' || !disCom.selectByValue(disCom.getValue(), false)) {
				field.setValue('');
			}
		}
	});

	disCom.setWidth(100);
	return disCom;
}


//===============================
// Html Edit를 생성한다.
//===============================
function createHtmlEdit(renderTo)
{
	var edit = new Ext.form.HtmlEditor({
	    renderTo:renderTo
	   // width: 740
	    //height: 300
	});


    return edit;
}
Ext.override(Ext.form.HtmlEditor, {
	onDisable: function(){
		if(this.rendered){
			this.wrap.mask();
		}
		Ext.form.HtmlEditor.superclass.onDisable.call(this);
	},
	onEnable: function(){
		if(this.rendered){
			this.wrap.unmask();
		}
		Ext.form.HtmlEditor.superclass.onEnable.call(this);
	}
});

//===============================
// 파일 업로드를 생성한다
//===============================
function createFileUpload(renderTo)
{
    var fibasic = new Ext.form.FileUploadField({
        renderTo: renderTo,
        id: renderTo,
        name: renderTo,
        buttonText: '찾기',
        width: 400
    });

    return fibasic;
}
//===============================
// 텍스트필드를 생성한다.(onBlur)
//===============================
function createTextFieldOnblur(applyTo, printTo, val, varName)
{
    var txt = new Ext.form.TextField({
           applyTo:applyTo,
           id:applyTo,
           name:applyTo,
           value:val,
           width: 100
    });

    txt.on('change', function(fn, scope, options) {
		var variable = fn.getValue();
		var upperVal = Ext.util.Format.uppercase(variable);

    	if(varName == "CUST_CD") {

    		txt.setValue(upperVal);
    		if(printTo != null){
				selectCustmDisc(upperVal, printTo);
			}
	    }else if(varName == "SCH_USER_ID") {

 			txt.setValue(variable);
 			if(printTo != null){
 				selectUserDisc(variable, printTo);
			}
	    }else if(varName == "SCH_MATERIAL_NO") {

 			txt.setValue(upperVal);
 			if(printTo != null){
 				selectItemCodeNm(upperVal, printTo);
			}
	    }else if(varName == "SCH_NATION_CD") {

 			txt.setValue(upperVal);
 			if(printTo != null){
 				selectNationNm(upperVal, printTo);
			}
	    }

    });

    return txt;
}


//===============================
//	트리초기화
//===============================
function treeinit(id) {
	//var frm = eval(id);
	id.innerHTML = '';
}

//===============================
//	트리생성
//===============================
function treeload(id, dataUrl, width, height, fucName,code, name) {

	//if(!width) width = 180;
	//if(!height) height = 350;
	if(!code) code = "";
	if(!name) name = "MENU";

	// 트리로더 정의
	Ext.app.OrgLoader = Ext.extend(Ext.ux.XmlTreeLoader, {
		processAttributes : function(attr){

			if (attr.CHILD <= 0) {
				attr.leaf = true;
			}

	        attr.text = attr.CD_NM;
	        attr.expanded = false;
		},
		processResponse : function(response, node, callback){

			var xmlData = response.responseXML;
			var root = Ext.DomQuery.selectNode("/EffectDTO/LtoList/RESULT/RowList", xmlData);

	        try{
				node.beginUpdate();
				node.appendChild(this.parseXml(root));
				node.endUpdate();

				if(typeof callback == "function") {
	            	callback(this, node);
				}
			}catch(e){
	        	this.handleFailure(response);
	        }
		}
	});

	// 메뉴로더
	var loader = new Ext.app.OrgLoader({
		dataUrl:dataUrl,
		baseParams: {PA_TREE_ID: code}
	});

	loader.on("beforeload", function(treeLoader,node) {
		if(node.attributes.TREE_ID != undefined && node.attributes.TREE_ID != "") {
			treeLoader.baseParams.PA_TREE_ID = node.attributes.TREE_ID;
		}
	},this);

	var treePanel = new Ext.Panel({
		renderTo: id,
        layout: 'border',
        width: width,
        height: height,
        expanded: true,
        items: [{
        	xtype: 'treepanel',
			id: 'tree-panel',
            region: 'center',
            autoScroll: true,
            //rootVisible: false,
            root: new Ext.tree.AsyncTreeNode({
            	text: name,
				id: 'ROOT',
				expanded : true
            }),
            loader: loader,
            listeners: {
				'render': function(tp){
					if(fucName) {
	                	tp.getSelectionModel().on('selectionchange', function(tree, node){
							//fucName.call(this, tree, node);
						})

						tp.on('click', function(node, event){
							fucName.call(this, '', node);
						})
					}


				}
			}
		}]
	});
	return treePanel;
}

//===============================
// Grid plugins
//===============================
Ext.grid.CheckColumn = function(config){
    Ext.apply(this, config);
    if(!this.id){
        this.id = Ext.id();
    }
    this.renderer = this.renderer.createDelegate(this);
};

Ext.grid.CheckColumn.prototype ={
    init : function(grid){
        this.grid = grid;
        this.grid.on('render', function(){
            var view = this.grid.getView();

            if(view.lockedBody) {
            	view.lockedBody.on('mousedown', this.onMouseDown, this);
            }else{
            	view.mainBody.on('mousedown', this.onMouseDown, this);
            }

        }, this);
    },


	onMouseDown : function(e, t){
        if(t.className && t.className.indexOf('x-grid3-cc-'+this.id) != -1){
            e.stopEvent();
            var index = this.grid.getView().findRowIndex(t);
            var record = this.grid.store.getAt(index);
            record.set(this.dataIndex, !record.data[this.dataIndex]);

	// fire afterEdit with all required params
            this.grid.fireEvent('afterEdit', {
				grid: this.grid,
				record: record,
				field: this.dataIndex,
				value: record.data[this.dataIndex],
				originalValue: !record.data[this.dataIndex],
				row: index,
				column: this.grid.getColumnModel().getIndexById(this.id)
	    	});
        }
    },
    renderer : function(v, p, record){
        p.css += ' x-grid3-check-col-td';
        return '<div class="x-grid3-check-col'+(v?'-on':'')+' x-grid3-cc-'+this.id+'">&#160;</div>';
    }
};

Ext.ns("Ext.ux.renderer");

Ext.ux.renderer.ComboRenderer = function(options) {
    var value = options.value;
    var combo = options.combo;

    var returnValue = value;
    var valueField = combo.valueField;

    var idx = combo.store.findBy(function(record) {
        if(record.get(valueField) == value) {
            returnValue = record.get(combo.displayField);
            return true;
        }
    });

    // This is our application specific and might need to be removed for your apps
    if(idx < 0 && value == 0) {
        returnValue = '';
    }

    return returnValue;
};

Ext.ux.renderer.Combo = function(combo) {
    return function(value, meta, record) {
        return Ext.ux.renderer.ComboRenderer({value: value, meta: meta, record: record, combo: combo});
    };
}

function doHeaderChkBoxClick(obj, grid , names) {
	var chk = false;

	if(!names) names = "CHK"
	if(obj.className == "x-grid3-check-col") {
		obj.className = "x-grid3-check-col-on"
		chk = true;
	}else{
		obj.className = "x-grid3-check-col"
		chk = false;
	}

 	for(var i=0; i < grid.store.getCount(); i++) {
    	var rec = grid.store.getAt(i);
		rec.set(names, chk);
    }

}

/*
 * 통문관리 공통코드 함수
 *
 */



//===============================
// 공통 콤보를 생성한다.
//===============================
function createCMAcombo(applyTo , emptyText, mainCode)
{
	if(!emptyText) emptyText = "전체";

    // 레코드 생성
	var recordDef = Ext.data.Record.create(['SUB_CODE','SUB_NAME']);
	var myStore = new Ext.data.Store({
		url: 'CMA01.R01.cmd',
		reader: new Ext.data.XmlReader({
            record: 'Row'
        }, recordDef)
	});

	var param = new Object();
	param['MAIN_CODE'] = mainCode;

	myStore.load({
		params: param
	});

	var disCom = new Ext.form.ComboBox({
		store: myStore,
		displayField:'SUB_NAME',
		valueField:'SUB_CODE',
		triggerAction: 'all',
		lazyRender:true,
		mode: 'local',
		emptyText: emptyText,
		style: publicStyle,
		applyTo: applyTo,
		editable:false

	});

// 기본 데이터 추가분
	if(emptyText=='전체'){
		var defaultData = {
				SUB_CODE : '',
				SUB_NAME : '전체'
			};

		var rec_Id = 100; 	// // provide unique id  IE9에서 0으로 설정시 안나옴
		var r = new myStore.recordType(defaultData, rec_Id);

		myStore.on('load', function(handler, scope){
			myStore.insert(0,r);
		});
	}
// 기본 데이터 추가분 끝

	disCom.on('blur', function(field) {
		if(applyTo) {
			if(Ext.get(applyTo).getValue() == emptyText || Ext.get(applyTo).getValue() == '' || !disCom.selectByValue(disCom.getValue(), false)) {
				field.setValue('');
			}
		}
	});

	disCom.setWidth(150);
	disCom.setReadOnly();
	return disCom;
}


function createCMAcombo1(applyTo , emptyText, mainCode)
{
	if(!emptyText) emptyText = "전체";

    // 레코드 생성
	var recordDef = Ext.data.Record.create(['SUB_CODE','SUB_NAME']);
	var myStore = new Ext.data.Store({
		url: 'CMA01.R01.cmd',
		reader: new Ext.data.XmlReader({
            record: 'Row'
        }, recordDef)
	});

	var param = new Object();
	param['MAIN_CODE'] = mainCode;

	myStore.load({
		params: param
	});

	var disCom = new Ext.form.ComboBox({
		store: myStore,
		displayField:'SUB_NAME',
		valueField:'SUB_CODE',
		triggerAction: 'all',
		lazyRender:true,
		mode: 'local',
		emptyText: emptyText,
		style: publicStyle,
		applyTo: applyTo,
		editable:false

	});

// 기본 데이터 추가분
	if(emptyText=='전체'){
		var defaultData = {
				SUB_CODE : '',
				SUB_NAME : '전체'
			};

		var rec_Id = 100; 	// // provide unique id  IE9에서 0으로 설정시 안나옴
		var r = new myStore.recordType(defaultData, rec_Id);

		myStore.on('load', function(handler, scope){
			myStore.insert(0,r);
		});
	}
// 기본 데이터 추가분 끝

	disCom.on('blur', function(field) {
		if(applyTo) {
			if(Ext.get(applyTo).getValue() == emptyText || Ext.get(applyTo).getValue() == '' || !disCom.selectByValue(disCom.getValue(), false)) {
				field.setValue('');
			}
		}
	});

	disCom.setWidth(150);
	// disCom.setReadOnly();
	return disCom;
}


//===============================
//사업장 콤보를 생성한다. 부천, 상우만 검색
//===============================

function createFabCombo(applyTo , emptyText)
{
	if(!emptyText) emptyText = "전체";

    // 레코드 생성
	var recordDef = Ext.data.Record.create(['SUB_CODE','SUB_NAME']);
	var myStore = new Ext.data.Store({
		url: 'CMA01.R02.cmd',
		reader: new Ext.data.XmlReader({
            record: 'Row'
        }, recordDef)
	});

	myStore.load();

	var disCom = new Ext.form.ComboBox({
		store: myStore,
		displayField:'SUB_NAME',
		valueField:'SUB_CODE',
		triggerAction: 'all',
		lazyRender:true,
		mode: 'local',
		emptyText: emptyText,
		style: publicStyle,
		applyTo: applyTo,
		editable:false

	});

// 기본 데이터 추가분
	if(emptyText=='전체'){
		var defaultData = {
				SUB_CODE : '',
				SUB_NAME : '전체'
			};

		var rec_Id = 100; 	// // provide unique id  IE9에서 0으로 설정시 안나옴
		var r = new myStore.recordType(defaultData, rec_Id);

		myStore.on('load', function(handler, scope){
			myStore.insert(0,r);
		});
	}
// 기본 데이터 추가분 끝

	disCom.on('blur', function(field) {
		if(applyTo) {
			if(Ext.get(applyTo).getValue() == emptyText || Ext.get(applyTo).getValue() == '' || !disCom.selectByValue(disCom.getValue(), false)) {
				field.setValue('');
			}
		}
	});

	disCom.setWidth(150);
	disCom.setReadOnly();
	return disCom;
}

// 대메뉴 콤보 ada01.jsp
function createFMenuCombo(applyTo, emptyText){

	if(!emptyText) emptyText = '전체';

	var recordDef = Ext.data.Record.create(['L_CODE','L_NAME']);
		var myStore = new Ext.data.Store({
		url: 'ADA01.R01.cmd',
		reader: new Ext.data.XmlReader({
            record: 'Row'
        }, recordDef)
	});
	myStore.load();
	var disCom = new Ext.form.ComboBox({
	    typeAhead: true,
	    triggerAction: 'all',
	    lazyRender:true,
	    mode: 'local',
	    emptyText: emptyText,
	    store: myStore,
	    valueField: 'L_CODE',
	    displayField: 'L_NAME',
	    applyTo : applyTo,
	    style: publicStyle,
		editable:false
	});

	var defaultData = {
			L_CODE : '',
			L_NAME : '전체'
		};
	var rec_Id = 100; 	// provide unique id  IE9에서 0으로 설정시 안나옴
	var r = new myStore.recordType(defaultData, rec_Id);
	myStore.on('load', function(handler, scope){
		myStore.insert(0,r);
	});

	disCom.on('blur', function(field) {
		if(applyTo) {
			if(Ext.get(applyTo).getValue() == emptyText || Ext.get(applyTo).getValue() == '' || !disCom.selectByValue(disCom.getValue(), false)) {
				field.setValue('');
			}
		}
	});

	disCom.setWidth(150);
	disCom.setReadOnly();
	return disCom;
}
