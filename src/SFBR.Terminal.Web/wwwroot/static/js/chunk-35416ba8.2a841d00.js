(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-35416ba8"],{"0197":function(t,e,a){"use strict";var n=a("fe03"),l=a.n(n);l.a},9404:function(t,e,a){"use strict";var n=a("d59e"),l=a.n(n);l.a},a237:function(t,e,a){"use strict";a.d(e,"E",(function(){return l})),a.d(e,"m",(function(){return r})),a.d(e,"s",(function(){return o})),a.d(e,"u",(function(){return c})),a.d(e,"b",(function(){return i})),a.d(e,"f",(function(){return s})),a.d(e,"h",(function(){return u})),a.d(e,"n",(function(){return d})),a.d(e,"C",(function(){return m})),a.d(e,"e",(function(){return v})),a.d(e,"w",(function(){return f})),a.d(e,"D",(function(){return p})),a.d(e,"q",(function(){return h})),a.d(e,"y",(function(){return b})),a.d(e,"g",(function(){return _})),a.d(e,"x",(function(){return g})),a.d(e,"c",(function(){return C})),a.d(e,"v",(function(){return k})),a.d(e,"i",(function(){return w})),a.d(e,"z",(function(){return j})),a.d(e,"j",(function(){return x})),a.d(e,"A",(function(){return O})),a.d(e,"k",(function(){return y})),a.d(e,"B",(function(){return T})),a.d(e,"l",(function(){return S})),a.d(e,"t",(function(){return F})),a.d(e,"o",(function(){return $})),a.d(e,"p",(function(){return z})),a.d(e,"r",(function(){return D})),a.d(e,"a",(function(){return B})),a.d(e,"d",(function(){return I}));var n=a("b775");function l(t){return Object(n["a"])({url:"/Terminal",method:"get",data:t})}function r(){return Object(n["a"])({url:"/Terminal/statistics/status",method:"get"})}function o(){return Object(n["a"])({url:"/Terminal/unpositioned",method:"get"})}function c(t,e){return Object(n["a"])({url:"/Terminal/"+t+"/location",method:"patch",data:e})}function i(){return Object(n["a"])({url:"/Terminal/statistics/camera/status",method:"get"})}function s(){return Object(n["a"])({url:"/Alarm/disposeStatus",method:"get"})}function u(){return Object(n["a"])({url:"/Alarm/level",method:"get"})}function d(t,e){return Object(n["a"])({url:"/Terminal/".concat(t,"/channel/").concat(e,"/task?a=1"),method:"get"})}function m(t,e,a){return Object(n["a"])({url:"/Terminal/".concat(t,"/channel/").concat(e,"/task"),method:"post",data:a})}function v(t){return Object(n["a"])({url:"/Terminal/".concat(t,"/defending"),method:"get"})}function f(t,e){return Object(n["a"])({url:"/Terminal/".concat(t,"/defending"),method:"post",data:e})}function p(t,e){return Object(n["a"])({url:"/Terminal/".concat(t,"/threshold"),method:"post",data:e})}function h(t){return Object(n["a"])({url:"/Terminal/".concat(t,"/threshold"),method:"get"})}function b(t,e,a){return Object(n["a"])({url:"/Terminal/".concat(t,"/camera/").concat(e,"/ip"),method:"post",data:a})}function _(t,e){return Object(n["a"])({url:"/Terminal/".concat(t,"/camera/").concat(e,"/ip"),method:"get"})}function g(t){return Object(n["a"])({url:"/Terminal/".concat(t,"/deviceTime"),method:"post"})}function C(t){return Object(n["a"])({url:"/Terminal/".concat(t,"/camera/checkfrequency"),method:"get"})}function k(t,e){return Object(n["a"])({url:"/Terminal/".concat(t,"/camera/checkfrequency"),method:"post",data:e})}function w(t,e){return Object(n["a"])({url:"/Terminal/".concat(t,"/channel/").concat(e,"/model"),method:"get"})}function j(t,e,a){return Object(n["a"])({url:"/Terminal/".concat(t,"/channel/").concat(e,"/model"),method:"post",data:a})}function x(t,e){return Object(n["a"])({url:"/Terminal/".concat(t,"/camera/").concat(e,"/mountPort"),method:"get"})}function O(t,e,a){return Object(n["a"])({url:"/Terminal/".concat(t,"/camera/").concat(e,"/mountPort"),method:"post",data:a})}function y(t){return Object(n["a"])({url:"/Terminal/".concat(t,"/position"),method:"get"})}function T(t,e,a){return Object(n["a"])({url:"/Terminal/".concat(t,"/channel/").concat(e,"/SwitchStatus"),method:"post",data:a})}function S(){return Object(n["a"])({url:"/Region/tree",method:"get"})}function F(t){return Object(n["a"])({url:"/Region/".concat(t,"/terminals"),method:"get"})}function $(t){return Object(n["a"])({url:"/Terminal/",method:"get",data:t})}function z(t){return Object(n["a"])({url:"/Terminal/".concat(t),method:"get"})}function D(){return Object(n["a"])({url:"/Alarm/topTime",method:"get"})}function B(t){return Object(n["a"])({url:"/Alarm",method:"get",data:t})}function I(t){return Object(n["a"])({url:"/Data/".concat(t),method:"get"})}},d59e:function(t,e,a){},f53d:function(t,e,a){"use strict";a.r(e);var n=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("section",{staticClass:"alarmmanagerPage"},[a("div",{staticClass:"alarmmanagerMain"},[a("div",{staticClass:"_btnBox"},[a("div",{staticClass:"lf_btn"},[a("button",{class:t.alarmBtn?"":"on",on:{click:function(e){t.alarmBtn=0}}},[t._v("未处理告警")]),t._v(" "),a("button",{class:t.alarmBtn?"on":"",on:{click:function(e){t.alarmBtn=1}}},[t._v("已处理告警")])]),t._v(" "),a("div",{staticClass:"rg_btn"},[a("button",[t._v("导入")]),t._v(" "),a("button",{on:{click:function(e){t.centerDialogVisible=!0}}},[t._v("派单")]),t._v(" "),a("div",{staticClass:"input_box"},[a("el-input",{attrs:{placeholder:"请输入内容"},model:{value:t.input,callback:function(e){t.input=e},expression:"input"}})],1),t._v(" "),a("button",[t._v("查询")]),t._v(" "),a("button",{staticClass:"search",class:t.searchShow?"active":"",on:{click:function(e){t.searchShow=!t.searchShow}}},[t._v("高级搜索")]),t._v(" "),a("Search",{directives:[{name:"show",rawName:"v-show",value:t.searchShow,expression:"searchShow"}]})],1)]),t._v(" "),a("div",{staticClass:"_tabBox",attrs:{id:"tableBox"}},[a("el-table",{attrs:{data:t.data.row,fit:"","max-height":t.heights,"highlight-current-row":""},on:{"row-click":t.tableChange}},[a("el-table-column",{attrs:{align:"center",label:"站点名称"},scopedSlots:t._u([{key:"default",fn:function(e){return[t._v(t._s(e.row.DeviceName))]}}])}),t._v(" "),a("el-table-column",{attrs:{align:"center",label:"站点编号"},scopedSlots:t._u([{key:"default",fn:function(e){return[t._v(t._s(e.row.EquipNum))]}}])}),t._v(" "),a("el-table-column",{attrs:{align:"center",label:"所属区域"},scopedSlots:t._u([{key:"default",fn:function(e){return[t._v(t._s(e.row.data))]}}])}),t._v(" "),a("el-table-column",{attrs:{align:"center",label:"报警类型"},scopedSlots:t._u([{key:"default",fn:function(e){return[t._v(t._s(e.row.AlarmType))]}}])}),t._v(" "),a("el-table-column",{attrs:{align:"center",label:"报警状态"},scopedSlots:t._u([{key:"default",fn:function(e){return[t._v(t._s(e.row.AlarmStatus))]}}])}),t._v(" "),a("el-table-column",{attrs:{align:"center",label:"报警级别"},scopedSlots:t._u([{key:"default",fn:function(e){return[t._v(t._s(e.row.AlarmLevel))]}}])}),t._v(" "),a("el-table-column",{attrs:{align:"center",label:"报警时间"},scopedSlots:t._u([{key:"default",fn:function(e){return[t._v(t._s(e.row.AlarmTime))]}}])})],1)],1),t._v(" "),a("div",{staticClass:"table_page"},[a("el-pagination",{attrs:{"current-page":t.page.currentPage,"page-sizes":[10,20,50,100],"page-size":t.page.pageSize,layout:"total, sizes, prev, pager, next, jumper",total:t.page.total},on:{"size-change":t.handleSizeChange,"current-change":t.handleCurrentChange}})],1)]),t._v(" "),a("el-dialog",{attrs:{title:"任务单配置","before-close":t.handleClose,visible:t.centerDialogVisible,"close-on-click-modal":!1,width:"800",modal:!0,"modal-append-to-body":!1,"destroy-on-close":!0,"append-to-body":!0},on:{"update:visible":function(e){t.centerDialogVisible=e}}},[t.centerDialogVisible?a("Dispatch",{on:{getMessage:t.stateMess}}):t._e()],1)],1)},l=[],r=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("section",{staticClass:"dispatchPage"},[a("div",{staticClass:"dispatchMain"},[a("el-form",{ref:"ruleForm",staticClass:"demo-ruleForm",attrs:{model:t.ruleForm,"label-width":"160px"}},[a("el-form-item",{attrs:{label:"工单编号："}},[a("el-input",{attrs:{placeholder:"请输入工单编号"},model:{value:t.ruleForm.name,callback:function(e){t.$set(t.ruleForm,"name",e)},expression:"ruleForm.name"}})],1),t._v(" "),a("el-form-item",{attrs:{label:"派单人员："}},[a("el-col",{attrs:{span:9}},[a("el-form-item",{attrs:{prop:"date1"}},[a("el-input",{attrs:{placeholder:"请输入派单人员"},model:{value:t.ruleForm.name,callback:function(e){t.$set(t.ruleForm,"name",e)},expression:"ruleForm.name"}})],1)],1),t._v(" "),a("el-col",{staticClass:"line",attrs:{span:6}},[t._v("站点名称：")]),t._v(" "),a("el-col",{attrs:{span:9}},[a("el-form-item",{attrs:{prop:"date2"}},[a("el-input",{attrs:{placeholder:"请输入站点名称"},model:{value:t.ruleForm.name,callback:function(e){t.$set(t.ruleForm,"name",e)},expression:"ruleForm.name"}})],1)],1)],1),t._v(" "),a("el-form-item",{attrs:{label:"所属区域："}},[a("el-col",{attrs:{span:9}},[a("el-form-item",{attrs:{prop:"date1"}},[a("el-input",{model:{value:t.ruleForm.name,callback:function(e){t.$set(t.ruleForm,"name",e)},expression:"ruleForm.name"}})],1)],1),t._v(" "),a("el-col",{staticClass:"line",attrs:{span:6}},[t._v("修复耗时：")]),t._v(" "),a("el-col",{attrs:{span:9}},[a("el-form-item",{attrs:{prop:"date2"}},[a("el-input",{model:{value:t.ruleForm.name,callback:function(e){t.$set(t.ruleForm,"name",e)},expression:"ruleForm.name"}})],1)],1)],1),t._v(" "),a("el-form-item",{attrs:{label:"报警描述："}},[a("el-col",{attrs:{span:9}},[a("el-form-item",{attrs:{prop:"date1"}},[a("el-select",{attrs:{placeholder:"请选择"},model:{value:t.value,callback:function(e){t.value=e},expression:"value"}},t._l(t.options,(function(t){return a("el-option",{key:t.value,attrs:{label:t.label,value:t.value}})})),1)],1)],1),t._v(" "),a("el-col",{staticClass:"line",attrs:{span:6}},[t._v("联系电话：")]),t._v(" "),a("el-col",{attrs:{span:9}},[a("el-form-item",{attrs:{prop:"date2"}},[a("el-input",{attrs:{placeholder:"请输入联系电话"},model:{value:t.ruleForm.name,callback:function(e){t.$set(t.ruleForm,"name",e)},expression:"ruleForm.name"}})],1)],1)],1),t._v(" "),a("el-form-item",{attrs:{label:"运维单位："}},[a("el-col",{attrs:{span:9}},[a("el-form-item",{attrs:{prop:"date1"}},[a("el-select",{attrs:{placeholder:"请选择"},model:{value:t.value,callback:function(e){t.value=e},expression:"value"}},t._l(t.options,(function(t){return a("el-option",{key:t.value,attrs:{label:t.label,value:t.value}})})),1)],1)],1),t._v(" "),a("el-col",{staticClass:"line",attrs:{span:6}},[t._v("运维人员：")]),t._v(" "),a("el-col",{attrs:{span:9}},[a("el-form-item",{attrs:{prop:"date2"}},[a("el-select",{attrs:{placeholder:"请选择"},model:{value:t.value,callback:function(e){t.value=e},expression:"value"}},t._l(t.options,(function(t){return a("el-option",{key:t.value,attrs:{label:t.label,value:t.value}})})),1)],1)],1)],1),t._v(" "),a("el-form-item",{attrs:{label:"是否关联警报："}},[a("el-col",{attrs:{span:9}},[a("el-form-item",{attrs:{prop:"date1"}},[a("el-select",{attrs:{placeholder:"请选择"},model:{value:t.value,callback:function(e){t.value=e},expression:"value"}},t._l(t.options,(function(t){return a("el-option",{key:t.value,attrs:{label:t.label,value:t.value}})})),1)],1)],1)],1),t._v(" "),a("el-form-item",{attrs:{label:"文本备注："}},[a("el-input",{attrs:{type:"textarea",autosize:{minRows:4,maxRows:8}},model:{value:t.ruleForm.name,callback:function(e){t.$set(t.ruleForm,"name",e)},expression:"ruleForm.name"}})],1),t._v(" "),a("el-form-item",[a("el-button",{attrs:{type:"primary"},on:{click:t.onSubmit}},[t._v("派单")]),t._v(" "),a("el-button",{on:{click:t.close}},[t._v("取消")])],1)],1)],1)])},o=[],c={name:"Dispatch",components:{},data:function(){return{ruleForm:{name:"",region:"",date1:"",date2:"",delivery:!1,type:[],resource:"",desc:""},options:[{value:"选项1",label:"黄金糕"}],value:""}},methods:{onSubmit:function(){console.log("submit!")},close:function(){var t=this;this.$confirm("确认关闭？").then((function(e){t.$emit("getMessage",!1)})).catch((function(t){}))}}},i=c,s=(a("0197"),a("2877")),u=Object(s["a"])(i,r,o,!1,null,"7b0b57a8",null),d=u.exports,m=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",{staticClass:"searchPage"},[a("div",{staticClass:"searchMain"},[a("div",{staticClass:"sarchItem"},[a("div",{staticClass:"tit"},[t._v("设备名称：")]),t._v(" "),a("div",{staticClass:"input"},[a("el-input",{attrs:{placeholder:"请输入内容"},model:{value:t.data.name,callback:function(e){t.$set(t.data,"name",e)},expression:"data.name"}})],1)]),t._v(" "),a("div",{staticClass:"sarchItem"},[a("div",{staticClass:"tit"},[t._v("设备IP：")]),t._v(" "),a("div",{staticClass:"input"},[a("el-input",{attrs:{placeholder:"请输入内容"},model:{value:t.data.ip,callback:function(e){t.$set(t.data,"ip",e)},expression:"data.ip"}})],1)]),t._v(" "),a("div",{staticClass:"sarchItem"},[a("div",{staticClass:"tit"},[t._v("所属区域：")]),t._v(" "),a("div",{staticClass:"input"},[a("el-select",{attrs:{placeholder:"请选择"},model:{value:t.data.addr,callback:function(e){t.$set(t.data,"addr",e)},expression:"data.addr"}},t._l(t.addrs,(function(t){return a("el-option",{key:t.value,attrs:{label:t.label,value:t.value}})})),1)],1)]),t._v(" "),a("div",{staticClass:"sarchItem"},[a("div",{staticClass:"tit"},[t._v("摄像机名称：")]),t._v(" "),a("div",{staticClass:"input"},[a("el-input",{attrs:{placeholder:"请输入内容"},model:{value:t.data.ip,callback:function(e){t.$set(t.data,"ip",e)},expression:"data.ip"}})],1)]),t._v(" "),a("div",{staticClass:"sarchItem"},[a("div",{staticClass:"tit"},[t._v("报警描述：")]),t._v(" "),a("div",{staticClass:"input"},[a("el-select",{attrs:{placeholder:"请选择"},model:{value:t.data.addr,callback:function(e){t.$set(t.data,"addr",e)},expression:"data.addr"}},t._l(t.addrs,(function(t){return a("el-option",{key:t.value,attrs:{label:t.label,value:t.value}})})),1)],1)]),t._v(" "),a("div",{staticClass:"sarchItem"},[a("div",{staticClass:"tit"},[t._v("设备状态：")]),t._v(" "),a("div",{staticClass:"input"},[a("el-select",{attrs:{placeholder:"请选择"},model:{value:t.data.addr,callback:function(e){t.$set(t.data,"addr",e)},expression:"data.addr"}},t._l(t.addrs,(function(t){return a("el-option",{key:t.value,attrs:{label:t.label,value:t.value}})})),1)],1)]),t._v(" "),a("div",{staticClass:"sarchItem"},[a("div",{staticClass:"tit"},[t._v("报警状态：")]),t._v(" "),a("div",{staticClass:"input"},[a("el-select",{attrs:{placeholder:"请选择"},model:{value:t.data.addr,callback:function(e){t.$set(t.data,"addr",e)},expression:"data.addr"}},t._l(t.addrs,(function(t){return a("el-option",{key:t.value,attrs:{label:t.label,value:t.value}})})),1)],1)]),t._v(" "),a("div",{staticClass:"sarchItem"},[a("div",{staticClass:"tit"},[t._v("派单状态：")]),t._v(" "),a("div",{staticClass:"input"},[a("el-select",{attrs:{placeholder:"请选择"},model:{value:t.data.addr,callback:function(e){t.$set(t.data,"addr",e)},expression:"data.addr"}},t._l(t.addrs,(function(t){return a("el-option",{key:t.value,attrs:{label:t.label,value:t.value}})})),1)],1)])]),t._v(" "),t._m(0)])},v=[function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",{staticClass:"btnMain"},[a("button",{staticClass:"btn_search"},[t._v("查询")]),t._v(" "),a("button",{staticClass:"btn_reset"},[t._v("重置")])])}],f={data:function(){return{data:{name:"",ip:"",addr:""},addrs:[{value:"选项1",label:"黄金糕"}]}}},p=f,h=(a("fa75"),Object(s["a"])(p,m,v,!1,null,"0d390886",null)),b=h.exports,_=a("a237"),g={name:"AlarmmanagerPage",components:{Dispatch:d,Search:b},data:function(){return{data:{row:[{name:"站点一号",type:"报修维护",data:"指令发送成功",admin:"操作人账号",name2:"王小二",time:"2019-06-27 08:26:35"}],tatal:1},page:{tatal:1,pageSize:10,currentPage:1},listLoading:!0,heights:parseInt(window.innerHeight-420),alarmBtn:0,input:"",currentRow:null,centerDialogVisible:!1,searchShow:!1}},watch:{alarmBtn:function(t,e){this.init()}},created:function(){this.init()},mounted:function(){var t=this;this.heights=window.document.getElementById("tableBox").clientHeight-151,window.onresize=function(){return function(){window.innerWidth>1500?t.heights=parseInt(window.innerHeight-420):t.heights=parseInt(window.innerHeight-640)}()}},methods:{init:function(){var t=this,e={page:this.page.currentPage,rows:this.page.pageSize,Disposed:this.alarmBtn};Object(_["a"])(e).then((function(e){t.page.tatal=e.Total,t.page.pageSize=e.Pages,t.page.currentPage=e.PageSize,t.data.row=e.Rows,t.data.tatal=e.Total}))},fetchData:function(){},handleSizeChange:function(t){console.log("每页 ".concat(t," 条"))},handleCurrentChange:function(t){console.log("当前页: ".concat(t))},tableChange:function(t){this.currentRow=t},handleClose:function(t){this.$confirm("确认关闭？").then((function(e){t()})).catch((function(t){}))},stateMess:function(t){console.log(t),this.centerDialogVisible=t}}},C=g,k=(a("9404"),Object(s["a"])(C,n,l,!1,null,"4b2b9489",null));e["default"]=k.exports},fa75:function(t,e,a){"use strict";var n=a("fb3d"),l=a.n(n);l.a},fb3d:function(t,e,a){},fe03:function(t,e,a){}}]);