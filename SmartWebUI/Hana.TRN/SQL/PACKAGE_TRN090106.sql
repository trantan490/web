/*<TOAD_FILE_CHUNK>*/
CREATE OR REPLACE PACKAGE RPTMGR.PACKAGE_TRN090106 IS
	/****************************************************
	 * COMMENT : PACKAGE별 매출현황을 조회한다.
	 *								
	 * CREATED BY : BEE-JAE JUNG(2016-06-16-목요일)
	 *											
	 * MODIFIED BY : BEE-JAE JUNG(2016-06-16-목요일)
	 ****************************************************/

	TYPE PKGCUR IS REF CURSOR;

	-- 2016-06-16-정비재 : PACKAGE별 매출현황을 조회한다.
	PROCEDURE PROC_TRN090106
	(
		P_WORK_MONTH		IN		VARCHAR2,		-- WORK_MONTH
		P_CUSTOMER			IN		VARCHAR2,		-- 고객사
		P_PACKAGE			IN		VARCHAR2,		-- PACKAGE
		PACKAGE_CURSOR		OUT		PKGCUR			-- 검색된 결과를 반환한다.
	);
	
END PACKAGE_TRN090106;


/************************************************************************************************************************************************************************************/
/************************************************************************************************************************************************************************************/
/
CREATE OR REPLACE PACKAGE BODY RPTMGR.PACKAGE_TRN090106 
AS

PROCEDURE PROC_TRN090106
(
		P_WORK_MONTH		IN		VARCHAR2,		-- WORK_MONTH
		P_CUSTOMER			IN		VARCHAR2,		-- 고객사
		P_PACKAGE			IN		VARCHAR2,		-- PACKAGE
		PACKAGE_CURSOR		OUT		PKGCUR			-- 검색된 결과를 반환한다.
)
/************************************************
 * COMMENT : EIS-PACKAGE별 매출현황
 *								
 * CREATED BY : BEE-JAE JUNG(2016-06-16-목요일)
 *											
 * MODIFIED BY : BEE-JAE JUNG(2016-06-16-목요일)
 ************************************************/

/********************************************************************************
 COMMENT : REF CURDOR로 RETURN되는 SQL문을 TOAD, ORANGE에서 실행결과 확인법
 
 BEGIN
     PACKAGE_TRN090106.PROC_TRN090106('201605', '%', '%', :CURSOR_OUT);
 END;
 ********************************************************************************/
IS
	-- 지역 변수 선언	
	P_PROC_NAME			VARCHAR2(30);
	P_PROC_DESC			VARCHAR2(50);
	P_START_TIME		VARCHAR2(14);
	P_END_TIME			VARCHAR2(14);
	P_EXE_COUNT			NUMBER;
    P_ERR_COUNT			NUMBER;
    
	P_TODATE_EOH		VARCHAR2(08);
	
    PROCEDURE_CURSOR	PKGCUR;
BEGIN

	-- 2016-06-16-정비재 : LOG기록을 위하여 변수에 데이터를 입력한다.
	P_PROC_NAME := 'PROC_TRN090106';
    P_PROC_DESC	:= 'EIS-PACKAGE별 매출현황';
    P_START_TIME := TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS');
    P_END_TIME := ' ';
    P_EXE_COUNT	:= 0;
    P_ERR_COUNT := 0;
    
	-- LOG 기록
	-- PROC를 시작할 때 LOG기록을 한 번 한다.(PROC의 RUNNING TIME을 CHECK하기 위하여)
	MESMGR.SET_INFO_STS_LOG@RPTTOMES(P_PROC_NAME, P_PROC_DESC, P_START_TIME, P_START_TIME, P_EXE_COUNT, P_ERR_COUNT, 'S');
	
	-- 2016-06-16-정비재 : 종료일자를 기준으로 하루 전 EOH 일자를 설정한다.
	P_TODATE_EOH := TO_CHAR(TO_DATE(P_WORK_MONTH || TO_CHAR(SYSDATE, 'DD'), 'YYYYMMDD') - 1, 'YYYYMMDD');

	-- 2016-06-16-정비재 : PACKAGE별 매출현황을 조회한다.
	OPEN PROCEDURE_CURSOR FOR
	/*
	SELECT WORK_MONTH AS WORK_MONTH
	     , CUSTOMER AS CUSTOMER
	     , PACKAGE AS PACKAGE
	     , SUM(RETURN_LOT) AS RETURN_LOT
	     , SUM(RETURN_QTY_1) AS RETURN_QTY_1
	     , SUM(RETURN_QTY_2) AS RETURN_QTY_2
	     , SUM(RETURN_KPRICE_1) AS RETURN_KPRICE_1
	     , SUM(RETURN_PRICE_1) AS RETURN_PRICE_1
	     , SUM(RECEIVE_LOT) AS RECEIVE_LOT
	     , SUM(RECEIVE_QTY_1) AS RECEIVE_QTY_1
	     , SUM(RECEIVE_QTY_2) AS RECEIVE_QTY_2
	     , SUM(RECEIVE_KPRICE_1) AS RECEIVE_KPRICE_1
	     , SUM(RECEIVE_PRICE_1) AS RECEIVE_PRICE_1
	     , SUM(ISSUE_LOT) AS ISSUE_LOT
	     , SUM(ISSUE_QTY_1) AS ISSUE_QTY_1
	     , SUM(ISSUE_QTY_2) AS ISSUE_QTY_2
	     , SUM(ISSUE_KPRICE_1) AS ISSUE_KPRICE_1
	     , SUM(ISSUE_PRICE_1) AS ISSUE_PRICE_1
	     , SUM(WIP_LOT) AS WIP_LOT
	     , SUM(WIP_QTY_1) AS WIP_QTY_1
	     , SUM(WIP_QTY_2) AS WIP_QTY_2
	     , SUM(WIP_KPRICE_1) AS WIP_KPRICE_1
	     , SUM(WIP_PRICE_1) AS WIP_PRICE_1
	     , SUM(SHIP_LOT) AS SHIP_LOT
	     , SUM(SHIP_QTY_1) AS SHIP_QTY_1
	     , SUM(SHIP_QTY_2) AS SHIP_QTY_2
	     , SUM(SHIP_KPRICE_1) AS SHIP_KPRICE_1
	     , SUM(SHIP_PRICE_1) AS SHIP_PRICE_1
	*/
	SELECT WORK_MONTH AS WORK_MONTH
	     , CUSTOMER AS CUSTOMER
	     , PACKAGE AS PACKAGE
	     , SUM(RETURN_QTY_1) AS RETURN_QTY_1
	     , SUM(RETURN_PRICE_1) AS RETURN_PRICE_1
	     , SUM(RECEIVE_QTY_1) AS RECEIVE_QTY_1
	     , SUM(RECEIVE_PRICE_1) AS RECEIVE_PRICE_1
	     , SUM(ISSUE_QTY_1) AS ISSUE_QTY_1
	     , SUM(ISSUE_PRICE_1) AS ISSUE_PRICE_1
	     , SUM(WIP_QTY_1) AS WIP_QTY_1
	     , SUM(WIP_PRICE_1) AS WIP_PRICE_1
	     , SUM(SHIP_QTY_1) AS SHIP_QTY_1
	     , SUM(SHIP_PRICE_1) AS SHIP_PRICE_1
	  FROM (-- RETURN LOT INFO
	  		SELECT A.FACTORY AS FACTORY
	             , A.WORK_MONTH AS WORK_MONTH
	             , B.MAT_GRP_1 AS CUSTOMER
	             , B.MAT_GRP_3 AS PACKAGE
	             , SUM(A.RETURN_LOT) AS RETURN_LOT
	             , SUM(A.RETURN_QTY_1) AS RETURN_QTY_1
	             , SUM(A.RETURN_QTY_2) AS RETURN_QTY_2
				 , SUM(A.RETURN_KPRICE_1) AS RETURN_KPRICE_1
	             , SUM(A.RETURN_PRICE_1) AS RETURN_PRICE_1
	             , 0 AS RECEIVE_LOT
	             , 0 AS RECEIVE_QTY_1
	             , 0 AS RECEIVE_QTY_2
	             , 0 AS RECEIVE_KPRICE_1
	             , 0 AS RECEIVE_PRICE_1
	             , 0 AS ISSUE_LOT
	             , 0 AS ISSUE_QTY_1
	             , 0 AS ISSUE_QTY_2
	             , 0 AS ISSUE_KPRICE_1
	             , 0 AS ISSUE_PRICE_1
	             , 0 AS WIP_LOT
	             , 0 AS WIP_QTY_1
	             , 0 AS WIP_QTY_2
	             , 0 AS WIP_KPRICE_1
	             , 0 AS WIP_PRICE_1
	             , 0 AS SHIP_LOT
	             , 0 AS SHIP_QTY_1
	             , 0 AS SHIP_QTY_2
	             , 0 AS SHIP_KPRICE_1
	             , 0 AS SHIP_PRICE_1
	          FROM (SELECT A.FACTORY AS FACTORY
	                     , A.WORK_MONTH AS WORK_MONTH
	                     , A.MAT_ID AS MAT_ID
	                     , A.MAT_VER AS MAT_VER
	                     , A.RETURN_LOT AS RETURN_LOT
	                     , A.RETURN_QTY_1 AS RETURN_QTY_1
	                     , A.RETURN_QTY_2 AS RETURN_QTY_2
	                     , NVL(B.KPRICE, 0) AS KPRICE
	                     , (A.RETURN_QTY_1 * NVL(B.KPRICE, 0)) AS RETURN_KPRICE_1
	                     , NVL(B.PRICE, 0) AS PRICE
	                     , (A.RETURN_QTY_1 * NVL(B.PRICE, 0)) AS RETURN_PRICE_1
	                  FROM (SELECT 'HMKA1' AS FACTORY
	                             , SUBSTR(WORK_DATE, 1, 6) AS WORK_MONTH
	                             , MAT_ID AS MAT_ID
	                             , MAT_VER AS MAT_VER
	                             , SUM(S1_OPER_IN_LOT   + S2_OPER_IN_LOT   + S3_OPER_IN_LOT) AS RETURN_LOT
	                             , SUM(S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1) AS RETURN_QTY_1
	                             , SUM(S1_OPER_IN_QTY_2 + S2_OPER_IN_QTY_2 + S3_OPER_IN_QTY_2) AS RETURN_QTY_2
	                          FROM RSUMWIPMOV A
	                         WHERE FACTORY = 'RETURN'
	                           AND CM_KEY_1 = 'HMKA1'
	                           AND LOT_TYPE = 'W'
	                           AND WORK_DATE LIKE P_WORK_MONTH || '%'
	                         GROUP BY SUBSTR(WORK_DATE, 1, 6), MAT_ID, MAT_VER) A
	                     , (SELECT PRODUCT AS MAT_ID
	                             , KPRICE AS KPRICE
	                             , PRICE AS PRICE
	                          FROM RPRIMATDAT) B
	                 WHERE A.MAT_ID = B.MAT_ID(+)) A, MWIPMATDEF B
	         WHERE A.FACTORY = B.FACTORY
	           AND A.MAT_ID = B.MAT_ID
	           AND A.MAT_VER = B.MAT_VER
	           AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	           AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	         GROUP BY A.FACTORY, A.WORK_MONTH, B.MAT_GRP_1, B.MAT_GRP_3
	        UNION ALL
	        -- RECEIVE LOT INFO
	        SELECT A.FACTORY AS FACTORY
	             , A.WORK_MONTH AS WORK_MONTH
	             , B.MAT_GRP_1 AS CUSTOMER
	             , B.MAT_GRP_3 AS PACKAGE
	             , 0 AS RETURN_LOT
	             , 0 AS RETURN_QTY_1
	             , 0 AS RETURN_QTY_2
				 , 0 AS RETURN_KPRICE_1
	             , 0 AS RETURN_PRICE_1
	             , SUM(A.RECEIVE_LOT) AS RECEIVE_LOT
	             , SUM(A.RECEIVE_QTY_1) AS RECEIVE_QTY_1
	             , SUM(A.RECEIVE_QTY_2) AS RECEIVE_QTY_2
	             , SUM(A.RECEIVE_KPRICE_1) AS RECEIVE_KPRICE_1
	             , SUM(A.RECEIVE_PRICE_1) AS RECEIVE_PRICE_1
	             , 0 AS ISSUE_LOT
	             , 0 AS ISSUE_QTY_1
	             , 0 AS ISSUE_QTY_2
	             , 0 AS ISSUE_KPRICE_1
	             , 0 AS ISSUE_PRICE_1
	             , 0 AS WIP_LOT
	             , 0 AS WIP_QTY_1
	             , 0 AS WIP_QTY_2
	             , 0 AS WIP_KPRICE_1
	             , 0 AS WIP_PRICE_1
	             , 0 AS SHIP_LOT
	             , 0 AS SHIP_QTY_1
	             , 0 AS SHIP_QTY_2
	             , 0 AS SHIP_KPRICE_1
	             , 0 AS SHIP_PRICE_1
	          FROM (SELECT A.FACTORY AS FACTORY
	                     , A.WORK_MONTH AS WORK_MONTH
	                     , A.MAT_ID AS MAT_ID
	                     , A.MAT_VER AS MAT_VER
	                     , A.RECEIVE_LOT AS RECEIVE_LOT
	                     , A.RECEIVE_QTY_1 AS RECEIVE_QTY_1
	                     , A.RECEIVE_QTY_2 AS RECEIVE_QTY_2
	                     , NVL(B.KPRICE, 0) AS KPRICE
	                     , (A.RECEIVE_QTY_1 * NVL(B.KPRICE, 0)) AS RECEIVE_KPRICE_1
	                     , NVL(B.PRICE, 0) AS PRICE
	                     , (A.RECEIVE_QTY_1 * NVL(B.PRICE, 0)) AS RECEIVE_PRICE_1
	                  FROM (SELECT FACTORY AS FACTORY
	                             , SUBSTR(WORK_DATE, 1, 6) AS WORK_MONTH
	                             , MAT_ID AS MAT_ID
	                             , MAT_VER AS MAT_VER 
	                             , SUM(S1_OPER_IN_LOT   + S2_OPER_IN_LOT   + S3_OPER_IN_LOT) AS RECEIVE_LOT
	                             , SUM(S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1) AS RECEIVE_QTY_1
	                             , SUM(S1_OPER_IN_QTY_2 + S2_OPER_IN_QTY_2 + S3_OPER_IN_QTY_2) AS RECEIVE_QTY_2
	                          FROM RSUMWIPMOV
	                         WHERE FACTORY = 'HMKA1'
	                           AND OPER = 'A0000'
	                           AND LOT_TYPE = 'W'
	                           AND WORK_DATE LIKE P_WORK_MONTH || '%'
	                         GROUP BY FACTORY, SUBSTR(WORK_DATE, 1, 6), MAT_ID, MAT_VER) A
	                     , (SELECT PRODUCT AS MAT_ID
	                             , KPRICE AS KPRICE
	                             , PRICE AS PRICE
	                          FROM RPRIMATDAT) B
	                 WHERE A.MAT_ID = B.MAT_ID(+)) A, MWIPMATDEF B
	         WHERE A.FACTORY = B.FACTORY
	           AND A.MAT_ID = B.MAT_ID
	           AND A.MAT_VER = B.MAT_VER
	           AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	           AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	         GROUP BY A.FACTORY, A.WORK_MONTH, B.MAT_GRP_1, B.MAT_GRP_3
	        UNION ALL
	        -- ISSUE LOT INFO
	        SELECT A.FACTORY AS FACTORY
	             , A.WORK_MONTH AS WORK_MONTH
	             , B.MAT_GRP_1 AS CUSTOMER
	             , B.MAT_GRP_3 AS PACKAGE
	             , 0 AS RETURN_LOT
	             , 0 AS RETURN_QTY_1
	             , 0 AS RETURN_QTY_2
	             , 0 AS RETURN_KPRICE_1
				 , 0 AS RETURN_PRICE_1
	             , 0 AS RECEIVE_LOT
	             , 0 AS RECEIVE_QTY_1
	             , 0 AS RECEIVE_QTY_2
	             , 0 AS RECEIVE_KPRICE_1
	             , 0 AS RECEIVE_PRICE_1
	             , SUM(A.ISSUE_LOT) AS ISSUE_LOT
	             , SUM(A.ISSUE_QTY_1) AS ISSUE_QTY_1
	             , SUM(A.ISSUE_QTY_2) AS ISSUE_QTY_2
	             , SUM(A.ISSUE_KPRICE_1) AS ISSUE_KPRICE_1
	             , SUM(A.ISSUE_PRICE_1) AS ISSUE_PRICE_1
	             , 0 AS WIP_LOT
	             , 0 AS WIP_QTY_1
	             , 0 AS WIP_QTY_2
	             , 0 AS WIP_KPRICE_1
	             , 0 AS WIP_PRICE_1
	             , 0 AS SHIP_LOT
	             , 0 AS SHIP_QTY_1
	             , 0 AS SHIP_QTY_2
	             , 0 AS SHIP_KPRICE_1
	             , 0 AS SHIP_PRICE_1
	          FROM (SELECT A.FACTORY AS FACTORY
	                     , A.WORK_MONTH AS WORK_MONTH
	                     , A.MAT_ID AS MAT_ID
	                     , A.MAT_VER AS MAT_VER
	                     , A.ISSUE_LOT AS ISSUE_LOT
	                     , A.ISSUE_QTY_1 AS ISSUE_QTY_1
	                     , A.ISSUE_QTY_2 AS ISSUE_QTY_2
	                     , NVL(B.KPRICE, 0) AS KPRICE
	                     , (A.ISSUE_QTY_1 * NVL(B.KPRICE, 0)) AS ISSUE_KPRICE_1
	                     , NVL(B.PRICE, 0) AS PRICE
	                     , (A.ISSUE_QTY_1 * NVL(B.PRICE, 0)) AS ISSUE_PRICE_1
	                  FROM (SELECT FACTORY AS FACTORY
	                             , SUBSTR(WORK_DATE, 1, 6) AS WORK_MONTH
	                             , MAT_ID AS MAT_ID
	                             , MAT_VER AS MAT_VER 
	                             , SUM(S1_END_LOT   + S2_END_LOT   + S3_END_LOT) AS ISSUE_LOT
	                             , SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1) AS ISSUE_QTY_1
	                             , SUM(S1_END_QTY_2 + S2_END_QTY_2 + S3_END_QTY_2) AS ISSUE_QTY_2
	                          FROM RSUMWIPMOV
	                         WHERE FACTORY = 'HMKA1'
	                           AND OPER = 'A0000'
	                           AND LOT_TYPE = 'W'
	                           AND WORK_DATE LIKE P_WORK_MONTH || '%'
	                         GROUP BY FACTORY, SUBSTR(WORK_DATE, 1, 6), MAT_ID, MAT_VER) A
	                     , (SELECT PRODUCT AS MAT_ID
	                             , KPRICE AS KPRICE
	                             , PRICE AS PRICE
	                          FROM RPRIMATDAT) B
	                 WHERE A.MAT_ID = B.MAT_ID(+)) A, MWIPMATDEF B
	         WHERE A.FACTORY = B.FACTORY
	           AND A.MAT_ID = B.MAT_ID
	           AND A.MAT_VER = B.MAT_VER
	           AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	           AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	         GROUP BY A.FACTORY, A.WORK_MONTH, B.MAT_GRP_1, B.MAT_GRP_3
	        UNION ALL
	        -- WIP LOT INFO
	        SELECT A.FACTORY AS FACTORY
	             , A.WORK_MONTH AS WORK_MONTH
	             , B.MAT_GRP_1 AS CUSTOMER
	             , B.MAT_GRP_3 AS PACKAGE
	             , 0 AS RETURN_LOT
	             , 0 AS RETURN_QTY_1
	             , 0 AS RETURN_QTY_2
	             , 0 AS RETURN_KPRICE_1
				 , 0 AS RETURN_PRICE_1
	             , 0 AS RECEIVE_LOT
	             , 0 AS RECEIVE_QTY_1
	             , 0 AS RECEIVE_QTY_2
	             , 0 AS RECEIVE_KPRICE_1
	             , 0 AS RECEIVE_PRICE_1
	             , 0 AS ISSUE_LOT
	             , 0 AS ISSUE_QTY_1
	             , 0 AS ISSUE_QTY_2
	             , 0 AS ISSUE_KPRICE_1
	             , 0 AS ISSUE_PRICE_1
	             , SUM(A.WIP_LOT) AS WIP_LOT
	             , SUM(A.WIP_QTY_1) AS WIP_QTY_1
	             , SUM(A.WIP_QTY_2) AS WIP_QTY_2
	             , SUM(A.WIP_KPRICE_1) AS WIP_KPRICE_1
	             , SUM(A.WIP_PRICE_1) AS WIP_PRICE_1
	             , 0 AS SHIP_LOT
	             , 0 AS SHIP_QTY_1
	             , 0 AS SHIP_QTY_2
	             , 0 AS SHIP_KPRICE_1
	             , 0 AS SHIP_PRICE_1
	          FROM (SELECT A.FACTORY AS FACTORY
	                     , A.WORK_MONTH AS WORK_MONTH
	                     , A.MAT_ID AS MAT_ID
	                     , A.MAT_VER AS MAT_VER
	                     , A.WIP_LOT AS WIP_LOT
	                     , A.WIP_QTY_1 AS WIP_QTY_1
	                     , A.WIP_QTY_2 AS WIP_QTY_2
	                     , NVL(B.KPRICE, 0) AS KPRICE
	                     , (A.WIP_QTY_1 * NVL(B.KPRICE, 0)) AS WIP_KPRICE_1
	                     , NVL(B.PRICE, 0) AS PRICE
	                     , (A.WIP_QTY_1 * NVL(B.PRICE, 0)) AS WIP_PRICE_1
	                  FROM (SELECT FACTORY AS FACTORY
	                             , SUBSTR(CUTOFF_DT, 1, 6) AS WORK_MONTH
	                             , MAT_ID AS MAT_ID
	                             , MAT_VER AS MAT_VER
	                             , COUNT(LOT_ID) AS WIP_LOT
	                             , SUM(QTY_1) AS WIP_QTY_1
	                             , SUM(QTY_2) AS WIP_QTY_2
	                          FROM RWIPLOTSTS_BOH
	                         WHERE FACTORY = 'HMKA1'
	                           AND OPER NOT IN ('A0000', 'AZ010')
	                           AND LOT_TYPE = 'W'
	                           AND LOT_DEL_FLAG = ' '
	                           AND CUTOFF_DT = P_TODATE_EOH || '22'
	                           AND SUBSTR(CUTOFF_DT, 9, 2) = '22'
	                         GROUP BY FACTORY, SUBSTR(CUTOFF_DT, 1, 6), MAT_ID, MAT_VER) A
	                     , (SELECT PRODUCT AS MAT_ID
	                             , KPRICE AS KPRICE
	                             , PRICE AS PRICE
	                          FROM RPRIMATDAT) B
	                 WHERE A.MAT_ID = B.MAT_ID(+)) A, MWIPMATDEF B
	         WHERE A.FACTORY = B.FACTORY
	           AND A.MAT_ID = B.MAT_ID
	           AND A.MAT_VER = B.MAT_VER
	           AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	           AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	         GROUP BY A.FACTORY, A.WORK_MONTH, B.MAT_GRP_1, B.MAT_GRP_3
	        UNION ALL
	        -- SHIP LOT INFO
	        SELECT A.FACTORY AS FACTORY
	             , A.WORK_MONTH AS WORK_MONTH
	             , B.MAT_GRP_1 AS CUSTOMER
	             , B.MAT_GRP_3 AS PACKAGE
	             , 0 AS RETURN_LOT
	             , 0 AS RETURN_QTY_1
	             , 0 AS RETURN_QTY_2
	             , 0 AS RETURN_KPRICE_1
				 , 0 AS RETURN_PRICE_1
	             , 0 AS RECEIVE_LOT
	             , 0 AS RECEIVE_QTY_1
	             , 0 AS RECEIVE_QTY_2
	             , 0 AS RECEIVE_KPRICE_1
	             , 0 AS RECEIVE_PRICE_1
	             , 0 AS ISSUE_LOT
	             , 0 AS ISSUE_QTY_1
	             , 0 AS ISSUE_QTY_2
	             , 0 AS ISSUE_KPRICE_1
	             , 0 AS ISSUE_PRICE_1
	             , 0 AS WIP_LOT
	             , 0 AS WIP_QTY_1
	             , 0 AS WIP_QTY_2
	             , 0 AS WIP_KPRICE_1
	             , 0 AS WIP_PRICE_1
	             , SUM(A.SHIP_LOT) AS SHIP_LOT
	             , SUM(A.SHIP_QTY_1) AS SHIP_QTY_1
	             , SUM(A.SHIP_QTY_2) AS SHIP_QTY_2
	             , SUM(A.SHIP_KPRICE_1) AS SHIP_KPRICE_1
	             , SUM(A.SHIP_PRICE_1) AS SHIP_PRICE_1
	          FROM (SELECT A.FACTORY AS FACTORY
	                     , A.WORK_MONTH AS WORK_MONTH
	                     , A.MAT_ID AS MAT_ID
	                     , A.MAT_VER AS MAT_VER
	                     , A.SHIP_LOT AS SHIP_LOT
	                     , A.SHIP_QTY_1 AS SHIP_QTY_1
	                     , A.SHIP_QTY_2 AS SHIP_QTY_2
	                     , NVL(B.KPRICE, 0) AS KPRICE
	                     , (A.SHIP_QTY_1 * NVL(B.KPRICE, 0)) AS SHIP_KPRICE_1
	                     , NVL(B.PRICE, 0) AS PRICE
	                     , (A.SHIP_QTY_1 * NVL(B.PRICE, 0)) AS SHIP_PRICE_1
	                  FROM (SELECT 'HMKA1' AS FACTORY
	                             , SUBSTR(WORK_DATE, 1, 6) AS WORK_MONTH
	                             , MAT_ID AS MAT_ID
	                             , MAT_VER AS MAT_VER
	                             , SUM(S1_OPER_IN_LOT   + S2_OPER_IN_LOT   + S3_OPER_IN_LOT) AS SHIP_LOT
	                             , SUM(S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1) AS SHIP_QTY_1
	                             , SUM(S1_OPER_IN_QTY_2 + S2_OPER_IN_QTY_2 + S3_OPER_IN_QTY_2) AS SHIP_QTY_2
	                          FROM RSUMWIPMOV
	                         WHERE FACTORY = 'CUSTOMER'
	                           AND CM_KEY_1 = 'HMKA1'
	                           AND LOT_TYPE = 'W'
	                           AND WORK_DATE LIKE P_WORK_MONTH || '%'
	                         GROUP BY SUBSTR(WORK_DATE, 1, 6), MAT_ID, MAT_VER) A
	                     , (SELECT PRODUCT AS MAT_ID
	                             , KPRICE AS KPRICE
	                             , PRICE AS PRICE
	                          FROM RPRIMATDAT) B
	                 WHERE A.MAT_ID = B.MAT_ID(+)) A, MWIPMATDEF B
	         WHERE A.FACTORY = B.FACTORY
	           AND A.MAT_ID = B.MAT_ID
	           AND A.MAT_VER = B.MAT_VER
	           AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	           AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	         GROUP BY A.FACTORY, A.WORK_MONTH, B.MAT_GRP_1, B.MAT_GRP_3)
	 GROUP BY WORK_MONTH, CUSTOMER, PACKAGE
	 ORDER BY WORK_MONTH ASC, CUSTOMER ASC, PACKAGE ASC;
	 
	PACKAGE_CURSOR := PROCEDURE_CURSOR;

	-- 2016-06-16-정비재 : LOG기록을 위하여 PROC 종료시간을 가져온다.
	P_END_TIME := TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS');
	-- PROC를 시작할 때 LOG기록을 한 번 한다.(PROC의 RUNNING TIME을 CHECK하기 위하여)
    MESMGR.SET_INFO_STS_LOG@RPTTOMES(P_PROC_NAME, P_PROC_DESC, P_START_TIME, P_END_TIME, P_EXE_COUNT, P_ERR_COUNT, 'E');
	-- 실행중에 발생하는 ERROR를 LOG TABLE에 저장한다.
	MESMGR.SAVE_LOG_INFO@RPTTOMES(P_PROC_NAME, P_PROC_DESC, P_START_TIME, P_END_TIME, P_EXE_COUNT, P_ERR_COUNT, SUBSTRB(SQLERRM, 1, 200));
	
EXCEPTION
    WHEN OTHERS THEN
    -- SQL 실행하면서 발생되는 ERROR는 별도로 저장한다.
    MESMGR.SAVE_LOG_ERROR@RPTTOMES(P_PROC_NAME, P_PROC_DESC, SUBSTRB(SQLERRM, 1, 200), 'Y');
	ROLLBACK;
END;

END PACKAGE_TRN090106;