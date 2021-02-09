CREATE OR REPLACE PACKAGE RPTMGR.PACKAGE_TRN090102 IS
	/****************************************************
	 * COMMENT : 공정별 재공이동 현황
	 *								
	 * CREATED BY : BEE-JAE JUNG(2010-08-26-목요일)
	 *											
	 * MODIFIED BY : BEE-JAE JUNG(2010-08-26-목요일)
	 ****************************************************/

	TYPE PKGCUR IS REF CURSOR;

	-- 01. PACKAGE 그룹별 재공이동현황
	PROCEDURE PROC_TRN090102_MASTER
	(
		P_CUSTOMER		IN		VARCHAR2,		-- 고객사
		P_PACKAGE		IN		VARCHAR2,		-- PACKAGE
		P_LOTTYPE		IN		VARCHAR2,		-- LOT TYPE
		P_KPCS			IN		CHAR,			-- KPCS
		PACKAGE_RESULT	OUT		PKGCUR			-- 검색된 결과를 반환한다.
	);
    
	-- 02. PACKAGE별 상세현황
	PROCEDURE PROC_TRN090102_DETAIL
	(
		P_CUSTOMER		IN		VARCHAR2,		-- 고객사
		P_PACKAGE		IN		VARCHAR2,		-- PACKAGE
		P_OPER			IN		VARCHAR2,		-- OPER
		P_LOTTYPE		IN		VARCHAR2,		-- LOT TYPE
		PACKAGE_RESULT	OUT		PKGCUR			-- 검색된 결과를 반환한다.
	);
		
END PACKAGE_TRN090102;


/****************************************************************************************************************************************/
/****************************************************************************************************************************************/


CREATE OR REPLACE PACKAGE BODY RPTMGR.PACKAGE_TRN090102
AS

PROCEDURE PROC_TRN090102_MASTER
(
		P_CUSTOMER		IN		VARCHAR2,		-- 고객사
		P_PACKAGE		IN		VARCHAR2,		-- PACKAGE
		P_LOTTYPE		IN		VARCHAR2,		-- LOT TYPE
		P_KPCS			IN		CHAR,			-- KPCS
		PACKAGE_RESULT	OUT		PKGCUR			-- 검색된 결과를 반환한다.
)
/****************************************************
 * COMMENT : 공정별 재공이동 현황(PACKAGE 그룹별)
 *								
 * CREATED BY : BEE-JAE JUNG(2010-08-27-금요일)
 *											
 * MODIFIED BY : BEE-JAE JUNG(2010-08-27-금요일)
 ****************************************************/

/******************************************************************************************** 
 COMMENT : REF CURDOR로 RETURN되는 SQL문을 TOAD, ORANGE에서 실행결과 확인법
 
 BEGIN
     PACKAGE_TRN090102.PROC_TRN090102_MASTER('SE', 'BOC%', 'P%', 'N', :OUT);
 END;
 ********************************************************************************************/
IS
	-- 지역 변수 선언	
	PROCEDURE_RESULT	PKGCUR;
BEGIN

	-- PACKAGE 그룹별 재공현황
	OPEN PROCEDURE_RESULT FOR
	SELECT A.CUSTOMER AS CUSTOMER
		 , A.PACKAGE AS PACKAGE
	     , A.OPER AS OPER
	     , A.WAIT_QTY AS WAIT_QTY
		 , A.START_QTY AS START_QTY
		 , A.HOLD_QTY AS HOLD_QTY
		 , A.REWORK_QTY AS REWORK_QTY
		 , A.WAIT_TIME AS WAIT_TIME
		 , A.START_TIME AS START_TIME
		 , A.HOLD_TIME AS HOLD_TIME
		 , A.REWORK_TIME AS REWORK_TIME
	  FROM (SELECT A.CUSTOMER AS CUSTOMER
	             , A.PACKAGE AS PACKAGE
	             , A.OPER  AS OPER
	         	 , CASE P_KPCS WHEN 'Y' THEN ROUND(SUM(A.WAIT_QTY) / 1000, 2) ELSE SUM(A.WAIT_QTY) END AS WAIT_QTY
	         	 , CASE P_KPCS WHEN 'Y' THEN ROUND(SUM(A.START_QTY) / 1000, 2) ELSE SUM(A.START_QTY) END AS START_QTY
	         	 , CASE P_KPCS WHEN 'Y' THEN ROUND(SUM(A.HOLD_QTY) / 1000, 2) ELSE SUM(A.HOLD_QTY) END AS HOLD_QTY
	             , CASE P_KPCS WHEN 'Y' THEN ROUND(SUM(A.REWORK_QTY) / 1000, 2) ELSE SUM(A.REWORK_QTY) END AS REWORK_QTY
				 , ROUND(SUM(A.WAIT_TIME), 2) AS WAIT_TIME
	             , ROUND(SUM(A.START_TIME), 2) AS START_TIME
				 , ROUND(SUM(A.HOLD_TIME), 2) AS HOLD_TIME
	             , ROUND(SUM(A.REWORK_TIME), 2) AS REWORK_TIME
	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , SUM(CASE A.START_FLAG WHEN ' ' THEN A.QTY_1 ELSE 0 END) AS WAIT_QTY
	                     , SUM(CASE A.START_FLAG WHEN 'Y' THEN A.QTY_1 ELSE 0 END) AS START_QTY
						 , SUM(CASE A.HOLD_FLAG  WHEN 'Y' THEN A.QTY_1 ELSE 0 END) AS HOLD_QTY
	                     , SUM(CASE A.RWK_FLAG   WHEN 'Y' THEN A.QTY_1 ELSE 0 END) AS REWORK_QTY
	                       -- 2010-09-07-정비재 : TIME은 시간으로 환산하여 표시한다.
					     , AVG(CASE A.START_FLAG WHEN ' ' THEN (SYSDATE - TO_DATE(A.OPER_IN_TIME, 'YYYYMMDDHH24MISS')) * 24 ELSE 0 END) AS WAIT_TIME
					     , AVG(CASE A.START_FLAG WHEN 'Y' THEN (SYSDATE - TO_DATE(A.START_TIME, 'YYYYMMDDHH24MISS')) * 24 ELSE 0 END) AS START_TIME
					     , AVG(CASE A.HOLD_FLAG  WHEN 'Y' THEN (SYSDATE - TO_DATE(A.LAST_TRAN_TIME, 'YYYYMMDDHH24MISS')) * 24 ELSE 0 END) AS HOLD_TIME
					     , AVG(CASE A.RWK_FLAG   WHEN 'Y' THEN (SYSDATE - TO_DATE(A.LAST_TRAN_TIME, 'YYYYMMDDHH24MISS')) * 24 ELSE 0 END) AS REWORK_TIME
	                  FROM RWIPLOTSTS A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND A.MAT_VER = B.MAT_VER
	                   AND A.LOT_TYPE = 'W'
	                   AND A.LOT_CMF_5 LIKE P_LOTTYPE || '%'
	                   AND A.LOT_DEL_FLAG = ' '
			           AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
					   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) A
	         GROUP BY A.CUSTOMER, A.PACKAGE, A.OPER
			UNION ALL
	        SELECT A.CUSTOMER AS CUSTOMER
	             , A.PACKAGE AS PACKAGE
	             , A.OPER AS OPER
	         	 , CASE P_KPCS WHEN 'Y' THEN ROUND(SUM(A.SHIP_QTY) / 1000, 2) ELSE SUM(A.SHIP_QTY) END AS WAIT_QTY
	             , 0 AS START_QTY
	             , 0 AS HOLD_QTY
	             , 0 AS REWORK_QTY
	             , 0 AS WAIT_TIME
	             , 0 AS START_TIME
	             , 0 AS HOLD_TIME
	             , 0 AS REWORK_TIME
	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.CM_KEY_1 AS OPER
	                     , SUM(A.S1_FAC_OUT_QTY_1 + A.S2_FAC_OUT_QTY_1 + A.S3_FAC_OUT_QTY_1) AS SHIP_QTY
	                     , SUM(A.S1_FAC_OUT_LOT + A.S2_FAC_OUT_LOT + A.S3_FAC_OUT_LOT) AS SHIP_LOT
	                  FROM RSUMFACMOV A, MESMGR.MWIPMATDEF@RPTTOMES B
	                 WHERE A.CM_KEY_1 = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND A.MAT_VER = B.MAT_VER
	                   --AND A.FACTORY IN ('HMKA1', 'HMKE1', 'HMKS1', 'HMKT1', 'FGS', 'CUSTOMER')
	                   AND A.FACTORY = 'CUSTOMER'		-- 고객사에 출하된 수량만 있으면 됨.
	                   AND A.LOT_TYPE = 'W'
	                   AND A.WORK_DATE LIKE TO_CHAR(SYSDATE, 'YYYYMM') || '%'
	                   AND A.CM_KEY_3 LIKE P_LOTTYPE || '%'
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
					   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.CM_KEY_1) A
	         GROUP BY A.CUSTOMER, A.PACKAGE, A.OPER) A
	 ORDER BY DECODE(A.OPER, 'HMKE1', 'EZ999', 'HMKA1', 'AZ999', 'HMKT1', 'TZ999', 'HMKS1', 'SZ999', 'FGS', 'FZ999', A.OPER) ASC, A.CUSTOMER ASC, A.PACKAGE ASC;

	PACKAGE_RESULT := PROCEDURE_RESULT;
	
EXCEPTION
    WHEN OTHERS THEN
	ROLLBACK;
END;


/****************************************************************************************************************************************/
/****************************************************************************************************************************************/


PROCEDURE PROC_TRN090102_DETAIL
(
		P_CUSTOMER		IN		VARCHAR2,		-- 고객사
		P_PACKAGE		IN		VARCHAR2,		-- PACKAGE
		P_OPER			IN		VARCHAR2,		-- OPER
		P_LOTTYPE		IN		VARCHAR2,		-- LOT TYPE
		PACKAGE_RESULT	OUT		PKGCUR			-- 검색된 결과를 반환한다.
)
/****************************************************
 * COMMENT : 공정별 재공이동 현황(LEADCOUNT 그룹별)
 *								
 * CREATED BY : BEE-JAE JUNG(2010-08-26-목요일)
 *											
 * MODIFIED BY : BEE-JAE JUNG(2010-08-27-금요일)
 ****************************************************/

/******************************************************************************************** 
 COMMENT : REF CURDOR로 RETURN되는 SQL문을 TOAD, ORANGE에서 실행결과 확인법
 
 BEGIN
     PACKAGE_TRN090102.PROC_TRN090102_DETAIL('SE', 'BOC', 'A2100', 'P', :OUT);
 END;
 ********************************************************************************************/
IS
	-- 지역 변수 선언	
	PROCEDURE_RESULT	PKGCUR;
BEGIN

	-- LEADCOUNT 그룹별 재공현황
	OPEN PROCEDURE_RESULT FOR
	SELECT A.MAT_ID AS MAT_ID
	     , A.LOT_ID AS LOT_ID
	     , A.FLOW AS FLOW
	     , A.OPER AS OPER
	     , A.LOT_TYPE  AS LOT_TYPE
	     , A.QTY_1 AS QTY_1
	     , A.WAIT_TIME AS WAIT_TIME
	     , A.START_TIME AS START_TIME
	     , A.HOLD_TIME AS HOLD_TIME
	     , A.REWORK_TIME AS REWORK_TIME
	  FROM (SELECT A.MAT_ID AS MAT_ID
	             , A.LOT_ID AS LOT_ID
	             , A.FLOW AS FLOW
	             , A.OPER AS OPER
	             , A.LOT_CMF_5 AS LOT_TYPE 
	         	 , A.QTY_1 AS QTY_1
	             , ROUND(CASE A.START_FLAG WHEN ' ' THEN (SYSDATE - TO_DATE(A.OPER_IN_TIME, 'YYYYMMDDHH24MISS')) * 24 ELSE 0 END, 2) AS WAIT_TIME
	             , ROUND(CASE A.START_FLAG WHEN 'Y' THEN (SYSDATE - TO_DATE(A.START_TIME, 'YYYYMMDDHH24MISS')) * 24 ELSE 0 END, 2) AS START_TIME
	             , ROUND(CASE A.HOLD_FLAG  WHEN 'Y' THEN (SYSDATE - TO_DATE(A.LAST_TRAN_TIME, 'YYYYMMDDHH24MISS')) * 24 ELSE 0 END, 2) AS HOLD_TIME
	             , ROUND(CASE A.RWK_FLAG   WHEN 'Y' THEN (SYSDATE - TO_DATE(A.LAST_TRAN_TIME, 'YYYYMMDDHH24MISS')) * 24 ELSE 0 END, 2) AS REWORK_TIME
	          FROM RWIPLOTSTS A, MESMGR.MWIPMATDEF@RPTTOMES B 
	         WHERE A.FACTORY = B.FACTORY
	           AND A.MAT_ID = B.MAT_ID
	           AND A.MAT_VER = B.MAT_VER
	           AND A.LOT_TYPE = 'W'
	           AND A.LOT_DEL_FLAG = ' '
	           AND A.OPER = P_OPER
	           AND A.LOT_CMF_5 LIKE P_LOTTYPE || '%'
	           AND B.MAT_GRP_1 = P_CUSTOMER
	           AND B.MAT_GRP_3 = P_PACKAGE) A
	 ORDER BY A.OPER ASC, A.WAIT_TIME DESC, A.START_TIME DESC, A.HOLD_TIME DESC, A.REWORK_TIME DESC;

	PACKAGE_RESULT := PROCEDURE_RESULT;
	
EXCEPTION
    WHEN OTHERS THEN
	ROLLBACK;
END;


/****************************************************************************************************************************************/
/****************************************************************************************************************************************/


END PACKAGE_TRN090102;
/
