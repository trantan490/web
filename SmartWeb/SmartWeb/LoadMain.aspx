<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoadMain.aspx.cs" Inherits="LoadMain" %>

<html>
<head runat="server">
    <title>:::하나마이크론㈜ MES Report Site</title>
    <link rel=stylesheet HREF="css/SmartWebStyle.css" type="text/css" />
    <script type="text/javascript" language="javascript" src="<%=Session["MAIN_PATH"] %>js/LoadMain.js"></script>
    <script type="text/javascript" language="javascript">
        var sMainPath = "<%=Session["MAIN_PATH"] %>";
        var sUid = "<%=Session["USERID"] %>";
        
        function PageClose ()
        {//GLBVAL_BOOL_KEY - LoadMain.js파일에 선언된전역변수(GetKey function에서 사용),Client가 F5키를 누르면 GLBVAL_BOOL_KEY값을 true로 변경          
            if(!GLBVAL_LOGOUT_KEY){//로그아웃버튼과 상관없이 화면 refresh
                 if(!GLBVAL_BOOL_KEY) {//onbeforeunload Event가 실행되고 그 Event가 새로고침(F5)이 아닐경우(=윈도우창닫기를 클릭한경우)
                    if (confirm("Do you want logout?")) {
                    document.location.href = "LogOut.aspx?sMainPath="+sMainPath+"&sUid="+sUid;
                  } else {
                      event.keyCode = 0;
                      event.cancelBubble = true;
                      GLBVAL_BOOL_KEY=false;
                      event.returnValue = "로그아웃과 동시에 현재 페이지를 닫습니다.";
                  }
                }
            } else{//로그아웃 버튼을 클릭하여 화면Refresh(GLBVAL_LOGOUT_KEY 값은 true)
                GLBVAL_LOGOUT_KEY=false;
                document.location.href = "Login.aspx";
            }
        }
        
	    cnt=0;
	    function popwindow()
	    {
	        if(cnt<1){//횟수 지정 디폴트 1회
		        cnt++;
            hideMenuAll();
	    } else
		    clearInterval(tid);
	    }
	    //setTimeout을 사용해도 된다.
	    tid=setInterval(popwindow,((1000*2))); //1000*n초후 popwindow함수 실행 - 재귀호출	    
        document.onkeydown = GetKey; // <F5,특수키>Function Key Control(LoadMain.js 참고)
        
        // 오른쪽 버튼 클릭을 못하게 하는 부분
//        if (window.event) document.captureEvents(Event.MOUSEUP); // mouse up 이벤트를 잡음 // 넷스케이프에서만 대문자 E.
//        document.oncontextmenu = nocontextmenu;  // IE5+ 용
//        document.onmousedown = norightclick;     // 다른 브라우저 용
        // 오른쪽 버튼 클릭을 못하게 하는 부분
        
        showload(); //화면MENU Load(LoadMain.js 참고)
    </script>
    
    <script event="LogOutClicked()" for="_Control1">
      var _TimeOut = form1._Control1.TimeOut;
      if (_TimeOut == "Y"){
            GLBVAL_LOGOUT_KEY = true;
            alert("You have been logged out in the Smart Web");
            document.location.href = "LogOut.aspx?sMainPath="+sMainPath+"&sUid="+sUid;           
      }else{
         if(confirm("Do you want logout?")){
            GLBVAL_LOGOUT_KEY = true;
            document.location.href = "LogOut.aspx?sMainPath="+sMainPath+"&sUid="+sUid;
         }else{
            GLBVAL_LOGOUT_KEY = false;
         }
       }
    </script>
</head>

<body bgcolor="#FFFFFF" marginwidth="0" marginheight="0" topmargin="0" leftmargin="0" valign=top scroll="no" onbeforeunload ="PageClose()">
    <form id="form1" method="post"  runat="server">
    <script type="text/javascript" src="CreateObj.aspx?UserIdName=<%=Session["USERID"] %>"></script>
    <script type="text/javascript" id="clientEventHandlersJS" language="javascript">
        
    </script>
    </form>
</body>
</html>
