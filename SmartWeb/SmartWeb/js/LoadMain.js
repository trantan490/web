/*
 * �ε��� �����ϱ�
 */
    function preLoading(targetObjName)
    {	
	    var targetObj = eval(targetObjName);
	    targetObj.location.href = GLB_PATH_PRELOADING;
    }

/*
 * �ε��� �����ϱ�
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
 * �μ��� ���� ���ڿ��� ��üŸ������ ��ȯ�ϱ�
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
	var MSG_EXPORT_NODATA = "��ȯ�� �����Ͱ� �����ϴ�.";
	var x = screen.availWidth;// - 10;
	var y = screen.availHeight;// - 38;
	/* Client Side���� �������� ����� MENU DIVISION TAG */
	document.write('<DIV id="menuLoadDiv" style="Z-INDEX: 100; VISIBILITY:   ; WIDTH: '+x+'px; POSITION: absolute; HEIGHT: '+y+'px; left:0px; top:0px" >'
				+'<table border="1" cellpadding="0" cellspacing="0" width="100%" height="100%" bordercolor="#ffffff" bordercolorlight="#cccccc" bordercolordark="#ffffff">'
				+'<tr><td bgcolor="#ffffff"><IFRAME name="menuLoadIfrm" id="menuLoadIfrm" src="'+GLB_PATH_PRELOADING+'" marginwidth="0" marginheight="0" frameborder="0" style="WIDTH: '+x+'px; HEIGHT: '+y+'px;" scrolling="no"></IFRAME>'
				+'</td></tr></table></DIV>');
/*
F5 Function keycode Control
        document.onkeydown = GetKey; �������־�� �Ѵ�. window.onkeydown="GetKey();" �ص� �ȴ�.
*/
    function GetKey()
    {
        // keycode for F5 function
        //���Ű F5�� �����ٸ� 
        
        if (window.event && window.event.keyCode == 116) 
        { 
            // Ű���� �齺���̽�Ű�� �����Ѵ�.
            GLBVAL_BOOL_KEY = true;
            window.event.keyCode = 8; 
        } 
        //// keycode for F11 function
        else if (window.event && window.event.keyCode == 122) 
        { 
            // Ű���� �齺���̽�Ű�� �����Ѵ�.
            GLBVAL_BOOL_KEY = true;
            window.event.keyCode = 8; 
        } 
        //// keycode for F4 function
        else if (window.event && window.event.keyCode == 114) 
        { 
            // Ű���� �齺���̽�Ű�� �����Ѵ�.
            GLBVAL_BOOL_KEY = true;
            window.event.keyCode = 8; 
        } 
        // Ű���� �齺���̽� Ű���̸�
        else if (window.event && window.event.keyCode == 8) 
        { 
            //event bubbling; 
            //�� ������Ʈ���� �̺�Ʈ�� �߻����� �� �̺�Ʈ�� �߻���Ų �ҽ�������Ʈ�������� 
            //�� ������Ʈ�� �����ϰ� �ִ� ���� ������Ʈ�� �̺�Ʈ�� ���ʷ� �н��ȴ�
            //��Ȥ �� ���� �����ְ� ���� ���� �ִµ� �׷��� ���̴°� event object�� cancelBubble �̶�� property �̴�.
            //�׷���, F5key�� ���� ã�Ƽ� ������ ���� ����. �׷��� Ű �̺�Ʈ�� �߻������� 
            //���� ������Ʈ�� �̺�Ʈ�� �н��Ǵ°��� ���� �ֱ� ���� cancelBubble�� ����Ѵ�.
            window.event.cancelBubble = true; //�⺻���� false;
            window.event.returnValue = false; //�̺�Ʈ���� �߻��� ���� false�� �����Ѵ�.
            return false; 
        } 
    }

/*
Right Mouse Click Control
        // ������ ��ư Ŭ���� ���ϰ� �ϴ� �κ�
        if (window.Event) // �ݽ������������� �빮�� E.
          document.captureEvents(Event.MOUSEUP); // mouse up �̺�Ʈ�� ����
�Լ� ȣ��.
        document.oncontextmenu = nocontextmenu;      // IE5+ ��
        document.onmousedown = norightclick;      // �ٸ� ������ ��
*/
    function nocontextmenu() // IE4������ ����, �ٸ� �������� ����
    {
       event.cancelBubble = true;
       event.returnValue = false;

       return false;
    }

    function norightclick(e)   // �ٸ� ��� ���������� �۵�
    {
       if (window.Event)   // �ٽ�, IE �Ǵ� NAV ?
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