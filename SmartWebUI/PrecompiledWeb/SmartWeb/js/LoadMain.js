/*
 * 로딩전 셋팅하기
 */
    function preLoading(targetObjName)
    {	
	    var targetObj = eval(targetObjName);
	    targetObj.location.href = GLB_PATH_PRELOADING;
    }

/*
 * 로딩후 셋팅하기
 */
    function postLoading(targetObjName)
    {	
	    for(var iii=0; iii< GLBARR_MENU_IFRM.length; iii++)
	    {
		    if (targetObjName != GLBARR_MENU_IFRM[iii])
		    {
			    var obj = eval(GLBARR_MENU_IFRM[iii]);
			    if (obj) obj.location.href = GLB_PATH_PRELOADING;
		    }
	    }
    }

    function hideMenuAll()
    {	
	    for(var iii=0; iii<GLBARR_MENU_DIV.length; iii++)
	    {
		    var obj  = makingStrToObj(GLBARR_MENU_DIV[iii]);
		    if (obj) obj.style.visibility = "hidden";
	    }
    }

/*
 * 인수로 받은 문자열을 객체타입으로 변환하기
 */
    function makingStrToObj(pStr)
    {
	    return document.all(eval("'"+pStr+"'"));
    }

    function showload()
    {
	    preLoading(GLBARR_MENU_IFRM[0]);
    }
	/* Global variables */
	var GLBARR_MENU_DIV = new Array("menuLoadDiv");
	var GLBARR_MENU_IFRM = new Array("menuLoadIfrm");
	
	/* Global variables For PATH */
	var GLB_PATH = "/";
	var GLB_PATH_IMAGES = "/Images/"; 
	var GLB_PATH_COMMONAPP = "/CommonApp/";
	var GLB_PATH_COMMONWEB = "/CommonWeb/";
	var GLB_PATH_PRELOADING = "preLoading.html";
	var GLB_PATH_LISTWINDOW = "/WIA_ListWindow.aspx"; 
	var GLB_PATH_LOADMAIN = "LoadMain.aspx";
	
	var GLBVAL_BOOL_KEY = false;
	var GLBVAL_LOGOUT_KEY = false;
	
	/* Global variables For MESSAGE */
	var MSG_EXPORT_NODATA = "변환할 데이터가 없습니다.";
	var x = screen.availWidth;// - 10;
	var y = screen.availHeight;// - 38;
	/* Client Side에서 공통으로 사용할 MENU DIVISION TAG */
	document.write('<DIV id="menuLoadDiv" style="Z-INDEX: 100; VISIBILITY:   ; WIDTH: '+x+'px; POSITION: absolute; HEIGHT: '+y+'px; left:0px; top:0px" >'
				+'<table border="1" cellpadding="0" cellspacing="0" width="100%" height="100%" bordercolor="#ffffff" bordercolorlight="#cccccc" bordercolordark="#ffffff">'
				+'<tr><td bgcolor="#ffffff"><IFRAME name="menuLoadIfrm" id="menuLoadIfrm" src="'+GLB_PATH_PRELOADING+'" marginwidth="0" marginheight="0" frameborder="0" style="WIDTH: '+x+'px; HEIGHT: '+y+'px;" scrolling="no"></IFRAME>'
				+'</td></tr></table></DIV>');
/*
F5 Function keycode Control
        document.onkeydown = GetKey; 지시해주어야 한다. window.onkeydown="GetKey();" 해도 된다.
*/
    function GetKey()
    {
        // keycode for F5 function
        //펑션키 F5을 눌렀다면 
        
        if (window.event && window.event.keyCode == 116) 
        { 
            // 키값을 백스페이스키로 변경한다.
            GLBVAL_BOOL_KEY = true;
            window.event.keyCode = 8; 
        } 
        //// keycode for F11 function
        else if (window.event && window.event.keyCode == 122) 
        { 
            // 키값을 백스페이스키로 변경한다.
            GLBVAL_BOOL_KEY = true;
            window.event.keyCode = 8; 
        } 
        //// keycode for F4 function
        else if (window.event && window.event.keyCode == 114) 
        { 
            // 키값을 백스페이스키로 변경한다.
            GLBVAL_BOOL_KEY = true;
            window.event.keyCode = 8; 
        } 
        // 키값이 백스페이스 키값이면
        else if (window.event && window.event.keyCode == 8) 
        { 
            //event bubbling; 
            //한 엘리먼트에서 이벤트가 발생했을 때 이벤트를 발생시킨 소스엘리먼트에서부터 
            //그 엘리먼트를 포함하고 있는 상위 엘리먼트로 이벤트가 차례로 패스된다
            //간혹 그 고리를 끊어주고 싶을 때가 있는데 그럴때 쓰이는게 event object의 cancelBubble 이라는 property 이다.
            //그래서, F5key만 값을 찾아서 제어할 수는 없다. 그래서 키 이벤트가 발생했을때 
            //상위 엘리먼트로 이벤트가 패스되는것을 끊어 주기 위해 cancelBubble을 사용한다.
            window.event.cancelBubble = true; //기본값은 false;
            window.event.returnValue = false; //이벤트에서 발생한 값을 false로 설정한다.
            return false; 
        } 
    }

/*
Right Mouse Click Control
        // 오른쪽 버튼 클릭을 못하게 하는 부분
        if (window.Event) // 넷스케이프에서만 대문자 E.
          document.captureEvents(Event.MOUSEUP); // mouse up 이벤트를 잡음
함수 호출.
        document.oncontextmenu = nocontextmenu;      // IE5+ 용
        document.onmousedown = norightclick;      // 다른 브라우저 용
*/
    function nocontextmenu() // IE4에서만 적용, 다른 브라우저는 무시
    {
       event.cancelBubble = true;
       event.returnValue = false;

       return false;
    }

    function norightclick(e)   // 다른 모든 브라우저에서 작동
    {
       if (window.Event)   // 다시, IE 또는 NAV ?
       {
          if (e.which == 2 || e.which == 3)
             return false;
       }
       else
          if (event.button == 2 || event.button == 3)
          {
             event.cancelBubble = true;
             event.returnValue = false;
             return false;
          }
       
    }       
     				        
    function winclose()
    {
        alert("Exit MES Web Client.");
    }