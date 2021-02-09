CREATE OR REPLACE PACKAGE RPTMGR.PACKAGE_PQC030115 IS
	/****************************************************
	 * COMMENT : 공정별불량현황을 조회한다.
	 *								
	 * CREATED BY : BEE-JAE JUNG(2010-06-24-목요일)
	 *											
	 * MODIFIED BY : BEE-JAE JUNG(2010-06-25-금요일)
	 ****************************************************/

	TYPE PKGCUR IS REF CURSOR;

	-- 01. 제품별 공정불량현황 PPM을 조회한다.
	PROCEDURE PROC_PQC030115_PPM
	(
		P_FACTORY		IN		VARCHAR2,			-- 공장구분
		P_BASE_MONTH	IN		VARCHAR2, 			-- 기준년월
		P_BASE_TIME		IN		VARCHAR2, 			-- 기준일자
		P_CUSTOMER		IN		VARCHAR2,			-- 고객사
		P_PACKAGE		IN		VARCHAR2,			-- PACKAGE
		P_OPER			IN		VARCHAR2,			-- 공정구분
		P_PREV_FLAG		IN		CHAR,				-- 전월TOP5기준 사용
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
	);

	-- 02. 제품별 공정불량현황 LRR을 조회한다.
	PROCEDURE PROC_PQC030115_LRR
	(
		P_FACTORY		IN		VARCHAR2,			-- 공장구분
		P_BASE_MONTH	IN		VARCHAR2, 			-- 기준년월
		P_BASE_TIME		IN		VARCHAR2, 			-- 기준일자
		P_CUSTOMER		IN		VARCHAR2,			-- 고객사
		P_PACKAGE		IN		VARCHAR2,			-- PACKAGE
		P_OPER			IN		VARCHAR2,			-- 공정구분
		P_PREV_FLAG		IN		CHAR,				-- 전월TOP5기준 사용
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
	);

	-- 03. 제품별 공정불량현황 YIELD을 조회한다.
	PROCEDURE PROC_PQC030115_YIELD
	(
		P_FACTORY		IN		VARCHAR2,			-- 공장구분
		P_BASE_MONTH	IN		VARCHAR2, 			-- 기준년월
		P_BASE_TIME		IN		VARCHAR2, 			-- 기준일자
		P_CUSTOMER		IN		VARCHAR2,			-- 고객사
		P_PACKAGE		IN		VARCHAR2,			-- PACKAGE
		P_OPER			IN		VARCHAR2,			-- 공정구분
		P_PREV_FLAG		IN		CHAR,				-- 전월TOP5기준 사용
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
	);
	
END PACKAGE_PQC030115;


/************************************************************************************************************************************************************************************/
/************************************************************************************************************************************************************************************/


CREATE OR REPLACE PACKAGE BODY RPTMGR.PACKAGE_PQC030115 
AS

PROCEDURE PROC_PQC030115_PPM
(
		P_FACTORY		IN		VARCHAR2,			-- 공장구분
		P_BASE_MONTH	IN		VARCHAR2, 			-- 기준년월
		P_BASE_TIME		IN		VARCHAR2, 			-- 기준일자
		P_CUSTOMER		IN		VARCHAR2,			-- 고객사
		P_PACKAGE		IN		VARCHAR2,			-- PACKAGE
		P_OPER			IN		VARCHAR2,			-- 공정구분
		P_PREV_FLAG		IN		CHAR,				-- 전월TOP5기준 사용
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
)
/************************************************
 * COMMENT : 제품별 공정불량현황 PPM을 조회한다.
 *								
 * CREATED BY : BEE-JAE JUNG(2010-06-24-목요일)
 *											
 * MODIFIED BY : BEE-JAE JUNG(2010-06-25-금요일)
 ************************************************/
 
/************************************************************************************************************
 COMMENT : REF CURDOR로 RETURN되는 SQL문을 TOAD, ORANGE에서 실행결과 확인법
 
 BEGIN
     PACKAGE_PQC030115.PROC_PQC030115_PPM('HMKA1', '201008', '20100831', '%', '%', '%', 'Y', :CURSOR_OUT);
 END;
 ************************************************************************************************************/
IS
	-- 지역 변수 선언	
	PROCEDURE_RESULT	PKGCUR;
	V_MONTH				INT;
	V_DISPLAY_COUNT		INT;
BEGIN

	-- 2010-06-24-정비재 : 전월불량 TOP5를 이용하기 위하여 추가함	
	IF P_PREV_FLAG = 'Y' THEN
		V_MONTH := -1;
	ELSE
		V_MONTH := -0;
	END IF;
	
	-- 2010-06-25-정비재 : 항목별 표시할 데이터 건수를 지정한다.
	V_DISPLAY_COUNT := 5;
 
 	-- 2010-06-25-정비재 : 제품별 공정별 불량현황_PPM(월별/주별/일별)
 	OPEN PROCEDURE_RESULT FOR
	SELECT A.CUSTOMER AS CUSTOMER 
		 , A.PACKAGE AS PACKAGE
		 , A.OPER AS OPER
		 , A.LOSS_CODE || ' ' || B.LOSS_DESC AS LOSS_CODE
	     , SUM(A.LOSS_PPM_MM_1) AS LOSS_PPM_MM_1
	     , SUM(A.LOSS_PPM_MM_2) AS LOSS_PPM_MM_2
	     , SUM(A.LOSS_PPM_MM_3) AS LOSS_PPM_MM_3
	     , SUM(A.LOSS_PPM_WW_1) AS LOSS_PPM_WW_1
	     , SUM(A.LOSS_PPM_WW_2) AS LOSS_PPM_WW_2
	     , SUM(A.LOSS_PPM_WW_3) AS LOSS_PPM_WW_3
	     , SUM(A.LOSS_PPM_WW_4) AS LOSS_PPM_WW_4
	     , SUM(A.LOSS_PPM_DD_1) AS LOSS_PPM_DD_1
	     , SUM(A.LOSS_PPM_DD_2) AS LOSS_PPM_DD_2
	     , SUM(A.LOSS_PPM_DD_3) AS LOSS_PPM_DD_3
	     , SUM(A.LOSS_PPM_DD_4) AS LOSS_PPM_DD_4
	     , SUM(A.LOSS_PPM_DD_5) AS LOSS_PPM_DD_5
	     , SUM(A.LOSS_PPM_DD_6) AS LOSS_PPM_DD_6
	     , SUM(A.LOSS_PPM_DD_7) AS LOSS_PPM_DD_7
	  FROM (SELECT A.CUSTOMER AS CUSTOMER
	             , A.PACKAGE AS PACKAGE
	             , A.OPER AS OPER
	             , A.LOSS_CODE AS LOSS_CODE
	             , CASE WHEN A.LOSS_QTY_MM_1 > 0 AND B.START_QTY_MM_1 > 0 THEN ROUND((A.LOSS_QTY_MM_1 * 1000000) / B.START_QTY_MM_1, 2) ELSE 0 END AS LOSS_PPM_MM_1
	             , CASE WHEN A.LOSS_QTY_MM_2 > 0 AND B.START_QTY_MM_2 > 0 THEN ROUND((A.LOSS_QTY_MM_2 * 1000000) / B.START_QTY_MM_2, 2) ELSE 0 END AS LOSS_PPM_MM_2
	             , CASE WHEN A.LOSS_QTY_MM_3 > 0 AND B.START_QTY_MM_3 > 0 THEN ROUND((A.LOSS_QTY_MM_3 * 1000000) / B.START_QTY_MM_3, 2) ELSE 0 END AS LOSS_PPM_MM_3
	             , 0 AS LOSS_PPM_WW_1
	             , 0 AS LOSS_PPM_WW_2
	             , 0 AS LOSS_PPM_WW_3
	             , 0 AS LOSS_PPM_WW_4
	             , 0 AS LOSS_PPM_DD_1
	             , 0 AS LOSS_PPM_DD_2
	             , 0 AS LOSS_PPM_DD_3
	             , 0 AS LOSS_PPM_DD_4
	             , 0 AS LOSS_PPM_DD_5
	             , 0 AS LOSS_PPM_DD_6
	             , 0 AS LOSS_PPM_DD_7
	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                     , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1
	                     , SUM(A.LOSS_QTY_MM_2) AS LOSS_QTY_MM_2
	                     , SUM(A.LOSS_QTY_MM_3) AS LOSS_QTY_MM_3
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM') THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_1 
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -1), 'YYYYMM') THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_2
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -0), 'YYYYMM') THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_3
	                          FROM RSUMLOTLSD A 
	                         WHERE A.FACTORY = P_FACTORY
	                           AND A.TRAN_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM')
	                           AND A.TRAN_MONTH <= P_BASE_MONTH
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                     , MESMGR.MWIPMATDEF@RPTTOMES B
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	             , (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , SUM(A.START_QTY_MM_1) AS START_QTY_MM_1
	                     , SUM(A.START_QTY_MM_2) AS START_QTY_MM_2
	                     , SUM(A.START_QTY_MM_3) AS START_QTY_MM_3
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM') THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_MM_1 
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -1), 'YYYYMM') THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_MM_2
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -0), 'YYYYMM') THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_MM_3
	                          FROM RSUMLOTLSM A 
	                         WHERE A.FACTORY = P_FACTORY
	                           AND A.TRAN_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM')
	                           AND A.TRAN_MONTH <= P_BASE_MONTH
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A
	                     , MESMGR.MWIPMATDEF@RPTTOMES B
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B
	             , (SELECT A.CUSTOMER AS CUSTOMER
	                     , A.PACKAGE AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                             , B.MAT_GRP_3 AS PACKAGE
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1 
	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL
	                          FROM (SELECT A.FACTORY AS FACTORY
	                                     , A.MAT_ID AS MAT_ID
	                                     , A.OPER AS OPER
	                                     , A.LOSS_CODE AS LOSS_CODE
	                                     , SUM(A.LOSS_QTY_1) AS LOSS_QTY_MM_1 
	                                  FROM RSUMLOTLSD A 
	                                 WHERE A.FACTORY = P_FACTORY
	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), V_MONTH), 'YYYYMM')
	                                   AND A.OPER LIKE P_OPER || '%'
	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                             , MESMGR.MWIPMATDEF@RPTTOMES B
	                         WHERE A.FACTORY = B.FACTORY
	                           AND A.MAT_ID = B.MAT_ID
	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	                     , (SELECT KEY_1 AS LOSS_CODE
	                             , DATA_1 AS LOSS_DESC
	                             , DATA_5 AS LOSS_OPER_DESC
	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES
	                         WHERE FACTORY = P_FACTORY
	                           AND TABLE_NAME = 'LOSS_CODE'
	                           AND DATA_1 <> 'ETC'
	                           AND DATA_2 <> 'Y') B
	                 WHERE A.LOSS_CODE = B.LOSS_CODE
	                   AND A.LOSS_LEVEL <= V_DISPLAY_COUNT) C
	         WHERE A.CUSTOMER = B.CUSTOMER
	           AND A.PACKAGE = B.PACKAGE
	           AND A.OPER = B.OPER
	           AND A.CUSTOMER = C.CUSTOMER
	           AND A.PACKAGE = C.PACKAGE
	           AND A.OPER = C.OPER
	           AND A.LOSS_CODE = C.LOSS_CODE
	        UNION ALL
	        SELECT A.CUSTOMER AS CUSTOMER
	             , A.PACKAGE AS PACKAGE
	             , A.OPER AS OPER
	             , A.LOSS_CODE AS LOSS_CODE
	             , 0 AS LOSS_PPM_MM_1
	             , 0 AS LOSS_PPM_MM_2
	             , 0 AS LOSS_PPM_MM_3
	             , CASE WHEN A.LOSS_QTY_WW_1 > 0 AND B.START_QTY_WW_1 > 0 THEN ROUND((A.LOSS_QTY_WW_1 * 1000000) / B.START_QTY_WW_1, 2) ELSE 0 END AS LOSS_PPM_WW_1
	             , CASE WHEN A.LOSS_QTY_WW_2 > 0 AND B.START_QTY_WW_2 > 0 THEN ROUND((A.LOSS_QTY_WW_2 * 1000000) / B.START_QTY_WW_2, 2) ELSE 0 END AS LOSS_PPM_WW_2
	             , CASE WHEN A.LOSS_QTY_WW_3 > 0 AND B.START_QTY_WW_3 > 0 THEN ROUND((A.LOSS_QTY_WW_3 * 1000000) / B.START_QTY_WW_3, 2) ELSE 0 END AS LOSS_PPM_WW_3
	             , CASE WHEN A.LOSS_QTY_WW_4 > 0 AND B.START_QTY_WW_4 > 0 THEN ROUND((A.LOSS_QTY_WW_4 * 1000000) / B.START_QTY_WW_4, 2) ELSE 0 END AS LOSS_PPM_WW_4
	             , 0 AS LOSS_PPM_DD_1
	             , 0 AS LOSS_PPM_DD_2
	             , 0 AS LOSS_PPM_DD_3
	             , 0 AS LOSS_PPM_DD_4
	             , 0 AS LOSS_PPM_DD_5
	             , 0 AS LOSS_PPM_DD_6
	             , 0 AS LOSS_PPM_DD_7
	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                     , SUM(A.LOSS_QTY_WW_1) AS LOSS_QTY_WW_1
	                     , SUM(A.LOSS_QTY_WW_2) AS LOSS_QTY_WW_2
	                     , SUM(A.LOSS_QTY_WW_3) AS LOSS_QTY_WW_3
	                     , SUM(A.LOSS_QTY_WW_4) AS LOSS_QTY_WW_4
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                            END THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_1
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                            END THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_2
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                            END THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_3
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 6, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 5, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 4, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 3, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 2, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 1, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 0, 'YYYYMMDD')
	                                                            END THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_4
	                          FROM RSUMLOTLSD A 
	                         WHERE A.FACTORY = P_FACTORY
	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                              END
	                           AND A.TRAN_TIME <= P_BASE_TIME
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                     ,  MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	             , (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , SUM(A.START_QTY_WW_1) AS START_QTY_WW_1
	                     , SUM(A.START_QTY_WW_2) AS START_QTY_WW_2
	                     , SUM(A.START_QTY_WW_3) AS START_QTY_WW_3
	                     , SUM(A.START_QTY_WW_4) AS START_QTY_WW_4
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                            END THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_WW_1
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                            END THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_WW_2
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                            END THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_WW_3
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 6, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 5, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 4, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 3, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 2, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 1, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 0, 'YYYYMMDD')
	                                                            END THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_WW_4
	                          FROM RSUMLOTLSM A 
	                         WHERE A.FACTORY = P_FACTORY
	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                              END
	                           AND A.TRAN_TIME <= P_BASE_TIME
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A
	                     ,  MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B
	             , (SELECT A.CUSTOMER AS CUSTOMER
	                     , A.PACKAGE AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                             , B.MAT_GRP_3 AS PACKAGE
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1 
	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL
	                          FROM (SELECT A.FACTORY AS FACTORY
	                                     , A.MAT_ID AS MAT_ID
	                                     , A.OPER AS OPER
	                                     , A.LOSS_CODE AS LOSS_CODE
	                                     , SUM(A.LOSS_QTY_1) AS LOSS_QTY_MM_1 
	                                  FROM RSUMLOTLSD A 
	                                 WHERE A.FACTORY = P_FACTORY
	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), V_MONTH), 'YYYYMM')
	                                   AND A.OPER LIKE P_OPER || '%'
	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                             , MESMGR.MWIPMATDEF@RPTTOMES B
	                         WHERE A.FACTORY = B.FACTORY
	                           AND A.MAT_ID = B.MAT_ID
	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	                     , (SELECT KEY_1 AS LOSS_CODE
	                             , DATA_1 AS LOSS_DESC
	                             , DATA_5 AS LOSS_OPER_DESC
	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES
	                         WHERE FACTORY = P_FACTORY
	                           AND TABLE_NAME = 'LOSS_CODE'
	                           AND DATA_1 <> 'ETC'
	                           AND DATA_2 <> 'Y') B
	                 WHERE A.LOSS_CODE = B.LOSS_CODE
	                   AND A.LOSS_LEVEL <= V_DISPLAY_COUNT) C
	         WHERE A.CUSTOMER = B.CUSTOMER
	           AND A.PACKAGE = B.PACKAGE
	           AND A.OPER = B.OPER
	           AND A.CUSTOMER = C.CUSTOMER
	           AND A.PACKAGE = C.PACKAGE
	           AND A.OPER = C.OPER
	           AND A.LOSS_CODE = C.LOSS_CODE
	        UNION ALL
			SELECT A.CUSTOMER AS CUSTOMER
	             , A.PACKAGE AS PACKAGE
	             , A.OPER AS OPER
	             , A.LOSS_CODE AS LOSS_CODE
	             , 0 AS LOSS_PPM_MM_1
	             , 0 AS LOSS_PPM_MM_2
	             , 0 AS LOSS_PPM_MM_3
	             , 0 AS LOSS_PPM_WW_1
	             , 0 AS LOSS_PPM_WW_2
	             , 0 AS LOSS_PPM_WW_3
	             , 0 AS LOSS_PPM_WW_4
	             , CASE WHEN A.LOSS_QTY_DD_1 > 0 AND B.START_QTY_DD_1 > 0 THEN ROUND((A.LOSS_QTY_DD_1 * 1000000) / B.START_QTY_DD_1, 2) ELSE 0 END AS LOSS_PPM_DD_1
	             , CASE WHEN A.LOSS_QTY_DD_2 > 0 AND B.START_QTY_DD_2 > 0 THEN ROUND((A.LOSS_QTY_DD_2 * 1000000) / B.START_QTY_DD_2, 2) ELSE 0 END AS LOSS_PPM_DD_2
	             , CASE WHEN A.LOSS_QTY_DD_3 > 0 AND B.START_QTY_DD_3 > 0 THEN ROUND((A.LOSS_QTY_DD_3 * 1000000) / B.START_QTY_DD_3, 2) ELSE 0 END AS LOSS_PPM_DD_3
	             , CASE WHEN A.LOSS_QTY_DD_4 > 0 AND B.START_QTY_DD_4 > 0 THEN ROUND((A.LOSS_QTY_DD_4 * 1000000) / B.START_QTY_DD_4, 2) ELSE 0 END AS LOSS_PPM_DD_4
	             , CASE WHEN A.LOSS_QTY_DD_5 > 0 AND B.START_QTY_DD_5 > 0 THEN ROUND((A.LOSS_QTY_DD_5 * 1000000) / B.START_QTY_DD_5, 2) ELSE 0 END AS LOSS_PPM_DD_5
	             , CASE WHEN A.LOSS_QTY_DD_6 > 0 AND B.START_QTY_DD_6 > 0 THEN ROUND((A.LOSS_QTY_DD_6 * 1000000) / B.START_QTY_DD_6, 2) ELSE 0 END AS LOSS_PPM_DD_6
	             , CASE WHEN A.LOSS_QTY_DD_7 > 0 AND B.START_QTY_DD_7 > 0 THEN ROUND((A.LOSS_QTY_DD_7 * 1000000) / B.START_QTY_DD_7, 2) ELSE 0 END AS LOSS_PPM_DD_7
	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                     , SUM(A.LOSS_QTY_DD_1) AS LOSS_QTY_DD_1
	                     , SUM(A.LOSS_QTY_DD_2) AS LOSS_QTY_DD_2
	                     , SUM(A.LOSS_QTY_DD_3) AS LOSS_QTY_DD_3
	                     , SUM(A.LOSS_QTY_DD_4) AS LOSS_QTY_DD_4
	                     , SUM(A.LOSS_QTY_DD_5) AS LOSS_QTY_DD_5
	                     , SUM(A.LOSS_QTY_DD_6) AS LOSS_QTY_DD_6
	                     , SUM(A.LOSS_QTY_DD_7) AS LOSS_QTY_DD_7
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_1 
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_2
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_3
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_4
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_5
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_6
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_7
	                          FROM RSUMLOTLSD A 
	                         WHERE A.FACTORY = P_FACTORY
	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                              END
	                           AND A.TRAN_TIME <= P_BASE_TIME
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                     , MESMGR.MWIPMATDEF@RPTTOMES B
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	             , (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , SUM(A.START_QTY_DD_1) AS START_QTY_DD_1
	                     , SUM(A.START_QTY_DD_2) AS START_QTY_DD_2
	                     , SUM(A.START_QTY_DD_3) AS START_QTY_DD_3
	                     , SUM(A.START_QTY_DD_4) AS START_QTY_DD_4
	                     , SUM(A.START_QTY_DD_5) AS START_QTY_DD_5
	                     , SUM(A.START_QTY_DD_6) AS START_QTY_DD_6
	                     , SUM(A.START_QTY_DD_7) AS START_QTY_DD_7
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_1 
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_2
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_3
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_4
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_5
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_6
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_7
	                          FROM RSUMLOTLSM A 
	                         WHERE A.FACTORY = P_FACTORY
	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                              END
	                           AND A.TRAN_TIME <= P_BASE_TIME
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A
	                     , MESMGR.MWIPMATDEF@RPTTOMES B
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B
	             , (SELECT A.CUSTOMER AS CUSTOMER
	                     , A.PACKAGE AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                             , B.MAT_GRP_3 AS PACKAGE
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1 
	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL
	                          FROM (SELECT A.FACTORY AS FACTORY
	                                     , A.MAT_ID AS MAT_ID
	                                     , A.OPER AS OPER
	                                     , A.LOSS_CODE AS LOSS_CODE
	                                     , SUM(A.LOSS_QTY_1) AS LOSS_QTY_MM_1 
	                                  FROM RSUMLOTLSD A 
	                                 WHERE A.FACTORY = P_FACTORY
	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), V_MONTH), 'YYYYMM')
	                                   AND A.OPER LIKE P_OPER || '%'
	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                             , MESMGR.MWIPMATDEF@RPTTOMES B
	                         WHERE A.FACTORY = B.FACTORY
	                           AND A.MAT_ID = B.MAT_ID
	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	                     , (SELECT KEY_1 AS LOSS_CODE
	                             , DATA_1 AS LOSS_DESC
	                             , DATA_5 AS LOSS_OPER_DESC
	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES
	                         WHERE FACTORY = P_FACTORY
	                           AND TABLE_NAME = 'LOSS_CODE'
	                           AND DATA_1 <> 'ETC'
	                           AND DATA_2 <> 'Y') B
	                 WHERE A.LOSS_CODE = B.LOSS_CODE
	                   AND A.LOSS_LEVEL <= V_DISPLAY_COUNT) C
	         WHERE A.CUSTOMER = B.CUSTOMER
	           AND A.PACKAGE = B.PACKAGE
	           AND A.OPER = B.OPER
	           AND A.CUSTOMER = C.CUSTOMER
	           AND A.PACKAGE = C.PACKAGE
	           AND A.OPER = C.OPER
	           AND A.LOSS_CODE = C.LOSS_CODE) A
		 , (SELECT KEY_1 AS LOSS_CODE
	             , DATA_1 AS LOSS_DESC
	             , DATA_5 AS LOSS_OPER_DESC
	          FROM MESMGR.MGCMTBLDAT@RPTTOMES
	         WHERE FACTORY = P_FACTORY
	           AND TABLE_NAME = 'LOSS_CODE'
	           AND DATA_1 <> 'ETC'
	           AND DATA_2 <> 'Y') B
	 WHERE A.LOSS_CODE = B.LOSS_CODE 
	 GROUP BY A.CUSTOMER, A.PACKAGE, A.OPER, A.LOSS_CODE || ' ' || B.LOSS_DESC
	 ORDER BY A.CUSTOMER, A.PACKAGE, A.OPER, A.LOSS_CODE || ' ' || B.LOSS_DESC;

	PACKAGE_RESULT := PROCEDURE_RESULT;
	
EXCEPTION
    WHEN OTHERS THEN
	ROLLBACK;
END;


/************************************************************************************************************************************************************************************/
/************************************************************************************************************************************************************************************/


PROCEDURE PROC_PQC030115_LRR
(
		P_FACTORY		IN		VARCHAR2,			-- 공장구분
		P_BASE_MONTH	IN		VARCHAR2, 			-- 기준년월
		P_BASE_TIME		IN		VARCHAR2, 			-- 기준일자
		P_CUSTOMER		IN		VARCHAR2,			-- 고객사
		P_PACKAGE		IN		VARCHAR2,			-- PACKAGE
		P_OPER			IN		VARCHAR2,			-- 공정구분
		P_PREV_FLAG		IN		CHAR,				-- 전월TOP5기준 사용
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
)
/************************************************
 * COMMENT : 제품별 공정불량현황 LRR을 조회한다.
 *								
 * CREATED BY : BEE-JAE JUNG(2010-06-24-목요일)
 *											
 * MODIFIED BY : BEE-JAE JUNG(2010-06-25-금요일)
 ************************************************/
 
/************************************************************************************************************
 COMMENT : REF CURDOR로 RETURN되는 SQL문을 TOAD, ORANGE에서 실행결과 확인법
 
 BEGIN
     PACKAGE_PQC030115.PROC_PQC030115_PPM('HMKA1', '201008', '20100831', '%', '%', '%', 'Y', :CURSOR_OUT);
 END;
 ************************************************************************************************************/
IS
	-- 지역 변수 선언	
	PROCEDURE_RESULT	PKGCUR;
	V_MONTH				INT;
	V_DISPLAY_COUNT		INT;
BEGIN

	-- 2010-06-24-정비재 : 전월불량 TOP5를 이용하기 위하여 추가함	
	IF P_PREV_FLAG = 'Y' THEN
		V_MONTH := -1;
	ELSE
		V_MONTH := -0;
	END IF;
 
 	-- 2010-06-25-정비재 : 항목별 표시할 데이터 건수를 지정한다.
 	V_DISPLAY_COUNT := 5;
 	
 	-- 2010-06-25-정비재 : 제품별 공정별불량현황_LRR(월별/주별/일별)
 	OPEN PROCEDURE_RESULT FOR
	SELECT A.CUSTOMER AS CUSTOMER 
		 , A.PACKAGE AS PACKAGE
		 , A.OPER AS OPER
		 , A.LOSS_CODE || ' ' || B.LOSS_DESC AS LOSS_CODE
	     , SUM(A.LOSS_PPM_MM_1) AS LOSS_PPM_MM_1
	     , SUM(A.LOSS_PPM_MM_2) AS LOSS_PPM_MM_2
	     , SUM(A.LOSS_PPM_MM_3) AS LOSS_PPM_MM_3
	     , SUM(A.LOSS_PPM_WW_1) AS LOSS_PPM_WW_1
	     , SUM(A.LOSS_PPM_WW_2) AS LOSS_PPM_WW_2
	     , SUM(A.LOSS_PPM_WW_3) AS LOSS_PPM_WW_3
	     , SUM(A.LOSS_PPM_WW_4) AS LOSS_PPM_WW_4
	     , SUM(A.LOSS_PPM_DD_1) AS LOSS_PPM_DD_1
	     , SUM(A.LOSS_PPM_DD_2) AS LOSS_PPM_DD_2
	     , SUM(A.LOSS_PPM_DD_3) AS LOSS_PPM_DD_3
	     , SUM(A.LOSS_PPM_DD_4) AS LOSS_PPM_DD_4
	     , SUM(A.LOSS_PPM_DD_5) AS LOSS_PPM_DD_5
	     , SUM(A.LOSS_PPM_DD_6) AS LOSS_PPM_DD_6
	     , SUM(A.LOSS_PPM_DD_7) AS LOSS_PPM_DD_7
	  FROM (SELECT A.CUSTOMER AS CUSTOMER
	             , A.PACKAGE AS PACKAGE
	             , A.OPER AS OPER
	             , A.LOSS_CODE AS LOSS_CODE
	             , CASE WHEN A.LOSS_QTY_MM_1 > 0 AND B.START_QTY_MM_1 > 0 THEN ROUND((A.LOSS_QTY_MM_1 * 100) / B.START_QTY_MM_1, 2) ELSE 0 END AS LOSS_PPM_MM_1
	             , CASE WHEN A.LOSS_QTY_MM_2 > 0 AND B.START_QTY_MM_2 > 0 THEN ROUND((A.LOSS_QTY_MM_2 * 100) / B.START_QTY_MM_2, 2) ELSE 0 END AS LOSS_PPM_MM_2
	             , CASE WHEN A.LOSS_QTY_MM_3 > 0 AND B.START_QTY_MM_3 > 0 THEN ROUND((A.LOSS_QTY_MM_3 * 100) / B.START_QTY_MM_3, 2) ELSE 0 END AS LOSS_PPM_MM_3
	             , 0 AS LOSS_PPM_WW_1
	             , 0 AS LOSS_PPM_WW_2
	             , 0 AS LOSS_PPM_WW_3
	             , 0 AS LOSS_PPM_WW_4
	             , 0 AS LOSS_PPM_DD_1
	             , 0 AS LOSS_PPM_DD_2
	             , 0 AS LOSS_PPM_DD_3
	             , 0 AS LOSS_PPM_DD_4
	             , 0 AS LOSS_PPM_DD_5
	             , 0 AS LOSS_PPM_DD_6
	             , 0 AS LOSS_PPM_DD_7
	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                     , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1
	                     , SUM(A.LOSS_QTY_MM_2) AS LOSS_QTY_MM_2
	                     , SUM(A.LOSS_QTY_MM_3) AS LOSS_QTY_MM_3
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM') THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_1 
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -1), 'YYYYMM') THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_2
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -0), 'YYYYMM') THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_3
	                          FROM RSUMLOTLSD A 
	                         WHERE A.FACTORY = P_FACTORY
	                           AND A.TRAN_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM')
	                           AND A.TRAN_MONTH <= P_BASE_MONTH
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                     , MESMGR.MWIPMATDEF@RPTTOMES B
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	             , (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , SUM(A.START_QTY_MM_1) AS START_QTY_MM_1
	                     , SUM(A.START_QTY_MM_2) AS START_QTY_MM_2
	                     , SUM(A.START_QTY_MM_3) AS START_QTY_MM_3
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM') THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_MM_1 
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -1), 'YYYYMM') THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_MM_2
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -0), 'YYYYMM') THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_MM_3
	                          FROM RSUMLOTLSM A 
	                         WHERE A.FACTORY = P_FACTORY
	                           AND A.TRAN_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM')
	                           AND A.TRAN_MONTH <= P_BASE_MONTH
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A
	                     , MESMGR.MWIPMATDEF@RPTTOMES B
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B
	             , (SELECT A.CUSTOMER AS CUSTOMER
	                     , A.PACKAGE AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                             , B.MAT_GRP_3 AS PACKAGE
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1 
	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL
	                          FROM (SELECT A.FACTORY AS FACTORY
	                                     , A.MAT_ID AS MAT_ID
	                                     , A.OPER AS OPER
	                                     , A.LOSS_CODE AS LOSS_CODE
	                                     , SUM(A.LOSS_LOT_QTY_1) AS LOSS_QTY_MM_1 
	                                  FROM RSUMLOTLSD A 
	                                 WHERE A.FACTORY = P_FACTORY
	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), V_MONTH), 'YYYYMM')
	                                   AND A.OPER LIKE P_OPER || '%'
	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                             , MESMGR.MWIPMATDEF@RPTTOMES B
	                         WHERE A.FACTORY = B.FACTORY
	                           AND A.MAT_ID = B.MAT_ID
	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	                     , (SELECT KEY_1 AS LOSS_CODE
	                             , DATA_1 AS LOSS_DESC
	                             , DATA_5 AS LOSS_OPER_DESC
	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES
	                         WHERE FACTORY = P_FACTORY
	                           AND TABLE_NAME = 'LOSS_CODE'
	                           AND DATA_1 <> 'ETC'
	                           AND DATA_2 <> 'Y') B
	                 WHERE A.LOSS_CODE = B.LOSS_CODE
	                   AND A.LOSS_LEVEL <= 5) C
	         WHERE A.CUSTOMER = B.CUSTOMER
	           AND A.PACKAGE = B.PACKAGE
	           AND A.OPER = B.OPER
	           AND A.CUSTOMER = C.CUSTOMER
	           AND A.PACKAGE = C.PACKAGE
	           AND A.OPER = C.OPER
	           AND A.LOSS_CODE = C.LOSS_CODE
	        UNION ALL
	        SELECT A.CUSTOMER AS CUSTOMER
	             , A.PACKAGE AS PACKAGE
	             , A.OPER AS OPER
	             , A.LOSS_CODE AS LOSS_CODE
	             , 0 AS LOSS_PPM_MM_1
	             , 0 AS LOSS_PPM_MM_2
	             , 0 AS LOSS_PPM_MM_3
	             , CASE WHEN A.LOSS_QTY_WW_1 > 0 AND B.START_QTY_WW_1 > 0 THEN ROUND((A.LOSS_QTY_WW_1 * 100) / B.START_QTY_WW_1, 2) ELSE 0 END AS LOSS_PPM_WW_1
	             , CASE WHEN A.LOSS_QTY_WW_2 > 0 AND B.START_QTY_WW_2 > 0 THEN ROUND((A.LOSS_QTY_WW_2 * 100) / B.START_QTY_WW_2, 2) ELSE 0 END AS LOSS_PPM_WW_2
	             , CASE WHEN A.LOSS_QTY_WW_3 > 0 AND B.START_QTY_WW_3 > 0 THEN ROUND((A.LOSS_QTY_WW_3 * 100) / B.START_QTY_WW_3, 2) ELSE 0 END AS LOSS_PPM_WW_3
	             , CASE WHEN A.LOSS_QTY_WW_4 > 0 AND B.START_QTY_WW_4 > 0 THEN ROUND((A.LOSS_QTY_WW_4 * 100) / B.START_QTY_WW_4, 2) ELSE 0 END AS LOSS_PPM_WW_4
	             , 0 AS LOSS_PPM_DD_1
	             , 0 AS LOSS_PPM_DD_2
	             , 0 AS LOSS_PPM_DD_3
	             , 0 AS LOSS_PPM_DD_4
	             , 0 AS LOSS_PPM_DD_5
	             , 0 AS LOSS_PPM_DD_6
	             , 0 AS LOSS_PPM_DD_7
	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                     , SUM(A.LOSS_QTY_WW_1) AS LOSS_QTY_WW_1
	                     , SUM(A.LOSS_QTY_WW_2) AS LOSS_QTY_WW_2
	                     , SUM(A.LOSS_QTY_WW_3) AS LOSS_QTY_WW_3
	                     , SUM(A.LOSS_QTY_WW_4) AS LOSS_QTY_WW_4
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                            END THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_1
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                            END THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_2
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                            END THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_3
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 6, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 5, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 4, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 3, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 2, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 1, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 0, 'YYYYMMDD')
	                                                            END THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_4
	                          FROM RSUMLOTLSD A 
	                         WHERE A.FACTORY = 'HMKA1'
	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                              END
	                           AND A.TRAN_TIME <= P_BASE_TIME
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                     ,  MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	             , (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , SUM(A.START_QTY_WW_1) AS START_QTY_WW_1
	                     , SUM(A.START_QTY_WW_2) AS START_QTY_WW_2
	                     , SUM(A.START_QTY_WW_3) AS START_QTY_WW_3
	                     , SUM(A.START_QTY_WW_4) AS START_QTY_WW_4
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                            END THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_WW_1
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                            END THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_WW_2
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                            END THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_WW_3
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 6, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 5, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 4, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 3, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 2, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 1, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 0, 'YYYYMMDD')
	                                                            END THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_WW_4
	                          FROM RSUMLOTLSM A 
	                         WHERE A.FACTORY = 'HMKA1'
	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                              END
	                           AND A.TRAN_TIME <= P_BASE_TIME
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A
	                     ,  MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B
	             , (SELECT A.CUSTOMER AS CUSTOMER
	                     , A.PACKAGE AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                             , B.MAT_GRP_3 AS PACKAGE
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1 
	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL
	                          FROM (SELECT A.FACTORY AS FACTORY
	                                     , A.MAT_ID AS MAT_ID
	                                     , A.OPER AS OPER
	                                     , A.LOSS_CODE AS LOSS_CODE
	                                     , SUM(A.LOSS_LOT_QTY_1) AS LOSS_QTY_MM_1 
	                                  FROM RSUMLOTLSD A 
	                                 WHERE A.FACTORY = P_FACTORY
	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), V_MONTH), 'YYYYMM')
	                                   AND A.OPER LIKE P_OPER || '%'
	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                             , MESMGR.MWIPMATDEF@RPTTOMES B
	                         WHERE A.FACTORY = B.FACTORY
	                           AND A.MAT_ID = B.MAT_ID
	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	                     , (SELECT KEY_1 AS LOSS_CODE
	                             , DATA_1 AS LOSS_DESC
	                             , DATA_5 AS LOSS_OPER_DESC
	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES
	                         WHERE FACTORY = P_FACTORY
	                           AND TABLE_NAME = 'LOSS_CODE'
	                           AND DATA_1 <> 'ETC'
	                           AND DATA_2 <> 'Y') B
	                 WHERE A.LOSS_CODE = B.LOSS_CODE
	                   AND A.LOSS_LEVEL <= V_DISPLAY_COUNT) C
	         WHERE A.CUSTOMER = B.CUSTOMER
	           AND A.PACKAGE = B.PACKAGE
	           AND A.OPER = B.OPER
	           AND A.CUSTOMER = C.CUSTOMER
	           AND A.PACKAGE = C.PACKAGE
	           AND A.OPER = C.OPER
	           AND A.LOSS_CODE = C.LOSS_CODE
	        UNION ALL
			SELECT A.CUSTOMER AS CUSTOMER
	             , A.PACKAGE AS PACKAGE
	             , A.OPER AS OPER
	             , A.LOSS_CODE AS LOSS_CODE
	             , 0 AS LOSS_PPM_MM_1
	             , 0 AS LOSS_PPM_MM_2
	             , 0 AS LOSS_PPM_MM_3
	             , 0 AS LOSS_PPM_WW_1
	             , 0 AS LOSS_PPM_WW_2
	             , 0 AS LOSS_PPM_WW_3
	             , 0 AS LOSS_PPM_WW_4
	             , CASE WHEN A.LOSS_QTY_DD_1 > 0 AND B.START_QTY_DD_1 > 0 THEN ROUND((A.LOSS_QTY_DD_1 * 100) / B.START_QTY_DD_1, 2) ELSE 0 END AS LOSS_PPM_DD_1
	             , CASE WHEN A.LOSS_QTY_DD_2 > 0 AND B.START_QTY_DD_2 > 0 THEN ROUND((A.LOSS_QTY_DD_2 * 100) / B.START_QTY_DD_2, 2) ELSE 0 END AS LOSS_PPM_DD_2
	             , CASE WHEN A.LOSS_QTY_DD_3 > 0 AND B.START_QTY_DD_3 > 0 THEN ROUND((A.LOSS_QTY_DD_3 * 100) / B.START_QTY_DD_3, 2) ELSE 0 END AS LOSS_PPM_DD_3
	             , CASE WHEN A.LOSS_QTY_DD_4 > 0 AND B.START_QTY_DD_4 > 0 THEN ROUND((A.LOSS_QTY_DD_4 * 100) / B.START_QTY_DD_4, 2) ELSE 0 END AS LOSS_PPM_DD_4
	             , CASE WHEN A.LOSS_QTY_DD_5 > 0 AND B.START_QTY_DD_5 > 0 THEN ROUND((A.LOSS_QTY_DD_5 * 100) / B.START_QTY_DD_5, 2) ELSE 0 END AS LOSS_PPM_DD_5
	             , CASE WHEN A.LOSS_QTY_DD_6 > 0 AND B.START_QTY_DD_6 > 0 THEN ROUND((A.LOSS_QTY_DD_6 * 100) / B.START_QTY_DD_6, 2) ELSE 0 END AS LOSS_PPM_DD_6
	             , CASE WHEN A.LOSS_QTY_DD_7 > 0 AND B.START_QTY_DD_7 > 0 THEN ROUND((A.LOSS_QTY_DD_7 * 100) / B.START_QTY_DD_7, 2) ELSE 0 END AS LOSS_PPM_DD_7
	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                     , SUM(A.LOSS_QTY_DD_1) AS LOSS_QTY_DD_1
	                     , SUM(A.LOSS_QTY_DD_2) AS LOSS_QTY_DD_2
	                     , SUM(A.LOSS_QTY_DD_3) AS LOSS_QTY_DD_3
	                     , SUM(A.LOSS_QTY_DD_4) AS LOSS_QTY_DD_4
	                     , SUM(A.LOSS_QTY_DD_5) AS LOSS_QTY_DD_5
	                     , SUM(A.LOSS_QTY_DD_6) AS LOSS_QTY_DD_6
	                     , SUM(A.LOSS_QTY_DD_7) AS LOSS_QTY_DD_7
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_1 
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_2
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_3
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_4
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_5
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_6
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.LOSS_LOT_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_7
	                          FROM RSUMLOTLSD A 
	                         WHERE A.FACTORY = P_FACTORY
	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                              END
	                           AND A.TRAN_TIME <= P_BASE_TIME
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                     , MESMGR.MWIPMATDEF@RPTTOMES B
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	             , (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , SUM(A.START_QTY_DD_1) AS START_QTY_DD_1
	                     , SUM(A.START_QTY_DD_2) AS START_QTY_DD_2
	                     , SUM(A.START_QTY_DD_3) AS START_QTY_DD_3
	                     , SUM(A.START_QTY_DD_4) AS START_QTY_DD_4
	                     , SUM(A.START_QTY_DD_5) AS START_QTY_DD_5
	                     , SUM(A.START_QTY_DD_6) AS START_QTY_DD_6
	                     , SUM(A.START_QTY_DD_7) AS START_QTY_DD_7
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_DD_1 
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_DD_2
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_DD_3
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_DD_4
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_DD_5
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_DD_6
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.START_LOT_QTY_1 ELSE 0 END) AS START_QTY_DD_7
	                          FROM RSUMLOTLSM A 
	                         WHERE A.FACTORY = P_FACTORY
	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                              END
	                           AND A.TRAN_TIME <= P_BASE_TIME
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A
	                     , MESMGR.MWIPMATDEF@RPTTOMES B
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B
	             , (SELECT A.CUSTOMER AS CUSTOMER
	                     , A.PACKAGE AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                             , B.MAT_GRP_3 AS PACKAGE
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1 
	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL
	                          FROM (SELECT A.FACTORY AS FACTORY
	                                     , A.MAT_ID AS MAT_ID
	                                     , A.OPER AS OPER
	                                     , A.LOSS_CODE AS LOSS_CODE
	                                     , SUM(A.LOSS_LOT_QTY_1) AS LOSS_QTY_MM_1 
	                                  FROM RSUMLOTLSD A 
	                                 WHERE A.FACTORY = P_FACTORY
	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), V_MONTH), 'YYYYMM')
	                                   AND A.OPER LIKE P_OPER || '%'
	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                             , MESMGR.MWIPMATDEF@RPTTOMES B
	                         WHERE A.FACTORY = B.FACTORY
	                           AND A.MAT_ID = B.MAT_ID
	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	                     , (SELECT KEY_1 AS LOSS_CODE
	                             , DATA_1 AS LOSS_DESC
	                             , DATA_5 AS LOSS_OPER_DESC
	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES
	                         WHERE FACTORY = P_FACTORY
	                           AND TABLE_NAME = 'LOSS_CODE'
	                           AND DATA_1 <> 'ETC'
	                           AND DATA_2 <> 'Y') B
	                 WHERE A.LOSS_CODE = B.LOSS_CODE
	                   AND A.LOSS_LEVEL <= V_DISPLAY_COUNT) C
	         WHERE A.CUSTOMER = B.CUSTOMER
	           AND A.PACKAGE = B.PACKAGE
	           AND A.OPER = B.OPER
	           AND A.CUSTOMER = C.CUSTOMER
	           AND A.PACKAGE = C.PACKAGE
	           AND A.OPER = C.OPER
	           AND A.LOSS_CODE = C.LOSS_CODE) A
		 , (SELECT KEY_1 AS LOSS_CODE
	             , DATA_1 AS LOSS_DESC
	             , DATA_5 AS LOSS_OPER_DESC
	          FROM MESMGR.MGCMTBLDAT@RPTTOMES
	         WHERE FACTORY = P_FACTORY
	           AND TABLE_NAME = 'LOSS_CODE'
	           AND DATA_1 <> 'ETC'
	           AND DATA_2 <> 'Y') B
	 WHERE A.LOSS_CODE = B.LOSS_CODE 
	 GROUP BY A.CUSTOMER, A.PACKAGE, A.OPER, A.LOSS_CODE || ' ' || B.LOSS_DESC
	 ORDER BY A.CUSTOMER, A.PACKAGE, A.OPER, A.LOSS_CODE || ' ' || B.LOSS_DESC;
		 
	PACKAGE_RESULT := PROCEDURE_RESULT;
	
EXCEPTION
    WHEN OTHERS THEN
	ROLLBACK;
END;


/************************************************************************************************************************************************************************************/
/************************************************************************************************************************************************************************************/


PROCEDURE PROC_PQC030115_YIELD
(
		P_FACTORY		IN		VARCHAR2,			-- 공장구분
		P_BASE_MONTH	IN		VARCHAR2, 			-- 기준년월
		P_BASE_TIME		IN		VARCHAR2, 			-- 기준일자
		P_CUSTOMER		IN		VARCHAR2,			-- 고객사
		P_PACKAGE		IN		VARCHAR2,			-- PACKAGE
		P_OPER			IN		VARCHAR2,			-- 공정구분
		P_PREV_FLAG		IN		CHAR,				-- 전월TOP5기준 사용
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
)
/************************************************
 * COMMENT : 제품별 공정불량현황 YIELD을 조회한다.
 *								
 * CREATED BY : BEE-JAE JUNG(2010-06-28-월요일)
 *											
 * MODIFIED BY : BEE-JAE JUNG(2010-06-28-월요일)
 ************************************************/
 
/************************************************************************************************************
 COMMENT : REF CURDOR로 RETURN되는 SQL문을 TOAD, ORANGE에서 실행결과 확인법
 
 BEGIN
     PACKAGE_PQC030115.PROC_PQC030115_YIELD('HMKA1', '201008', '20100831', '%', '%', '%', 'Y', :CURSOR_OUT);
 END;
 ************************************************************************************************************/
IS
	-- 지역 변수 선언	
	PROCEDURE_RESULT	PKGCUR;
	V_MONTH				INT;
	V_DISPLAY_COUNT		INT;
BEGIN

	-- 2010-06-28-정비재 : 전월불량 TOP5를 이용하기 위하여 추가함	
	IF P_PREV_FLAG = 'Y' THEN
		V_MONTH := -1;
	ELSE
		V_MONTH := -0;
	END IF;
	
	-- 2010-06-28-정비재 : 항목별 표시할 데이터 건수를 지정한다.
	V_DISPLAY_COUNT := 5;
 
 	-- 2010-06-28-정비재 : 제품별 공정별 불량현황_YIELD(월별/주별/일별)
 	OPEN PROCEDURE_RESULT FOR
	SELECT A.CUSTOMER AS CUSTOMER 
		 , A.PACKAGE AS PACKAGE
		 , A.OPER AS OPER
		 , A.LOSS_CODE || ' ' || B.LOSS_DESC AS LOSS_CODE
	     , SUM(A.LOSS_PPM_MM_1) AS LOSS_PPM_MM_1
	     , SUM(A.LOSS_PPM_MM_2) AS LOSS_PPM_MM_2
	     , SUM(A.LOSS_PPM_MM_3) AS LOSS_PPM_MM_3
	     , SUM(A.LOSS_PPM_WW_1) AS LOSS_PPM_WW_1
	     , SUM(A.LOSS_PPM_WW_2) AS LOSS_PPM_WW_2
	     , SUM(A.LOSS_PPM_WW_3) AS LOSS_PPM_WW_3
	     , SUM(A.LOSS_PPM_WW_4) AS LOSS_PPM_WW_4
	     , SUM(A.LOSS_PPM_DD_1) AS LOSS_PPM_DD_1
	     , SUM(A.LOSS_PPM_DD_2) AS LOSS_PPM_DD_2
	     , SUM(A.LOSS_PPM_DD_3) AS LOSS_PPM_DD_3
	     , SUM(A.LOSS_PPM_DD_4) AS LOSS_PPM_DD_4
	     , SUM(A.LOSS_PPM_DD_5) AS LOSS_PPM_DD_5
	     , SUM(A.LOSS_PPM_DD_6) AS LOSS_PPM_DD_6
	     , SUM(A.LOSS_PPM_DD_7) AS LOSS_PPM_DD_7
	  FROM (SELECT A.CUSTOMER AS CUSTOMER
	             , A.PACKAGE AS PACKAGE
	             , A.OPER AS OPER
	             , A.LOSS_CODE AS LOSS_CODE
	             , CASE WHEN A.LOSS_QTY_MM_1 > 0 AND B.START_QTY_MM_1 > 0 THEN ROUND((A.LOSS_QTY_MM_1 * 100) / B.START_QTY_MM_1, 2) ELSE 0 END AS LOSS_PPM_MM_1
	             , CASE WHEN A.LOSS_QTY_MM_2 > 0 AND B.START_QTY_MM_2 > 0 THEN ROUND((A.LOSS_QTY_MM_2 * 100) / B.START_QTY_MM_2, 2) ELSE 0 END AS LOSS_PPM_MM_2
	             , CASE WHEN A.LOSS_QTY_MM_3 > 0 AND B.START_QTY_MM_3 > 0 THEN ROUND((A.LOSS_QTY_MM_3 * 100) / B.START_QTY_MM_3, 2) ELSE 0 END AS LOSS_PPM_MM_3
	             , 0 AS LOSS_PPM_WW_1
	             , 0 AS LOSS_PPM_WW_2
	             , 0 AS LOSS_PPM_WW_3
	             , 0 AS LOSS_PPM_WW_4
	             , 0 AS LOSS_PPM_DD_1
	             , 0 AS LOSS_PPM_DD_2
	             , 0 AS LOSS_PPM_DD_3
	             , 0 AS LOSS_PPM_DD_4
	             , 0 AS LOSS_PPM_DD_5
	             , 0 AS LOSS_PPM_DD_6
	             , 0 AS LOSS_PPM_DD_7
	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                     , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1
	                     , SUM(A.LOSS_QTY_MM_2) AS LOSS_QTY_MM_2
	                     , SUM(A.LOSS_QTY_MM_3) AS LOSS_QTY_MM_3
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM') THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_1 
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -1), 'YYYYMM') THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_2
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -0), 'YYYYMM') THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_MM_3
	                          FROM RSUMLOTLSD A 
	                         WHERE A.FACTORY = P_FACTORY
	                           AND A.TRAN_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM')
	                           AND A.TRAN_MONTH <= P_BASE_MONTH
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                     , MESMGR.MWIPMATDEF@RPTTOMES B
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	             , (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , SUM(A.START_QTY_MM_1) AS START_QTY_MM_1
	                     , SUM(A.START_QTY_MM_2) AS START_QTY_MM_2
	                     , SUM(A.START_QTY_MM_3) AS START_QTY_MM_3
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM') THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_MM_1 
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -1), 'YYYYMM') THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_MM_2
	                             , SUM(CASE A.TRAN_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -0), 'YYYYMM') THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_MM_3
	                          FROM RSUMLOTLSM A 
	                         WHERE A.FACTORY = P_FACTORY
	                           AND A.TRAN_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM')
	                           AND A.TRAN_MONTH <= P_BASE_MONTH
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A
	                     , MESMGR.MWIPMATDEF@RPTTOMES B
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B
	             , (SELECT A.CUSTOMER AS CUSTOMER
	                     , A.PACKAGE AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                             , B.MAT_GRP_3 AS PACKAGE
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1 
	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL
	                          FROM (SELECT A.FACTORY AS FACTORY
	                                     , A.MAT_ID AS MAT_ID
	                                     , A.OPER AS OPER
	                                     , A.LOSS_CODE AS LOSS_CODE
	                                     , SUM(A.LOSS_QTY_1) AS LOSS_QTY_MM_1 
	                                  FROM RSUMLOTLSD A 
	                                 WHERE A.FACTORY = P_FACTORY
	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), V_MONTH), 'YYYYMM')
	                                   AND A.OPER LIKE P_OPER || '%'
	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                             , MESMGR.MWIPMATDEF@RPTTOMES B
	                         WHERE A.FACTORY = B.FACTORY
	                           AND A.MAT_ID = B.MAT_ID
	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	                     , (SELECT KEY_1 AS LOSS_CODE
	                             , DATA_1 AS LOSS_DESC
	                             , DATA_5 AS LOSS_OPER_DESC
	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES
	                         WHERE FACTORY = P_FACTORY
	                           AND TABLE_NAME = 'LOSS_CODE'
	                           AND DATA_1 <> 'ETC'
	                           AND DATA_2 <> 'Y') B
	                 WHERE A.LOSS_CODE = B.LOSS_CODE
	                   AND A.LOSS_LEVEL <= V_DISPLAY_COUNT) C
	         WHERE A.CUSTOMER = B.CUSTOMER
	           AND A.PACKAGE = B.PACKAGE
	           AND A.OPER = B.OPER
	           AND A.CUSTOMER = C.CUSTOMER
	           AND A.PACKAGE = C.PACKAGE
	           AND A.OPER = C.OPER
	           AND A.LOSS_CODE = C.LOSS_CODE
	        UNION ALL
	        SELECT A.CUSTOMER AS CUSTOMER
	             , A.PACKAGE AS PACKAGE
	             , A.OPER AS OPER
	             , A.LOSS_CODE AS LOSS_CODE
	             , 0 AS LOSS_PPM_MM_1
	             , 0 AS LOSS_PPM_MM_2
	             , 0 AS LOSS_PPM_MM_3
	             , CASE WHEN A.LOSS_QTY_WW_1 > 0 AND B.START_QTY_WW_1 > 0 THEN ROUND((A.LOSS_QTY_WW_1 * 100) / B.START_QTY_WW_1, 2) ELSE 0 END AS LOSS_PPM_WW_1
	             , CASE WHEN A.LOSS_QTY_WW_2 > 0 AND B.START_QTY_WW_2 > 0 THEN ROUND((A.LOSS_QTY_WW_2 * 100) / B.START_QTY_WW_2, 2) ELSE 0 END AS LOSS_PPM_WW_2
	             , CASE WHEN A.LOSS_QTY_WW_3 > 0 AND B.START_QTY_WW_3 > 0 THEN ROUND((A.LOSS_QTY_WW_3 * 100) / B.START_QTY_WW_3, 2) ELSE 0 END AS LOSS_PPM_WW_3
	             , CASE WHEN A.LOSS_QTY_WW_4 > 0 AND B.START_QTY_WW_4 > 0 THEN ROUND((A.LOSS_QTY_WW_4 * 100) / B.START_QTY_WW_4, 2) ELSE 0 END AS LOSS_PPM_WW_4
	             , 0 AS LOSS_PPM_DD_1
	             , 0 AS LOSS_PPM_DD_2
	             , 0 AS LOSS_PPM_DD_3
	             , 0 AS LOSS_PPM_DD_4
	             , 0 AS LOSS_PPM_DD_5
	             , 0 AS LOSS_PPM_DD_6
	             , 0 AS LOSS_PPM_DD_7
	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                     , SUM(A.LOSS_QTY_WW_1) AS LOSS_QTY_WW_1
	                     , SUM(A.LOSS_QTY_WW_2) AS LOSS_QTY_WW_2
	                     , SUM(A.LOSS_QTY_WW_3) AS LOSS_QTY_WW_3
	                     , SUM(A.LOSS_QTY_WW_4) AS LOSS_QTY_WW_4
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                            END THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_1
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                            END THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_2
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                            END THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_3
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 6, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 5, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 4, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 3, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 2, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 1, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 0, 'YYYYMMDD')
	                                                            END THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_WW_4
	                          FROM RSUMLOTLSD A 
	                         WHERE A.FACTORY = P_FACTORY
	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                              END
	                           AND A.TRAN_TIME <= P_BASE_TIME
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                     ,  MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	             , (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , SUM(A.START_QTY_WW_1) AS START_QTY_WW_1
	                     , SUM(A.START_QTY_WW_2) AS START_QTY_WW_2
	                     , SUM(A.START_QTY_WW_3) AS START_QTY_WW_3
	                     , SUM(A.START_QTY_WW_4) AS START_QTY_WW_4
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                            END THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_WW_1
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                            END THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_WW_2
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                            END THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_WW_3
	                             , SUM(CASE WHEN A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                            END
	                                         AND A.TRAN_TIME <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 6, 'YYYYMMDD')
	                                                                                                                 WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 5, 'YYYYMMDD')
	                                                                                                                 WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 4, 'YYYYMMDD')
	                                                                                                                 WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 3, 'YYYYMMDD')
	                                                                                                                 WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 2, 'YYYYMMDD')
	                                                                                                                 WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 1, 'YYYYMMDD')
	                                                                                                                 WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 0, 'YYYYMMDD')
	                                                            END THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_WW_4
	                          FROM RSUMLOTLSM A 
	                         WHERE A.FACTORY = P_FACTORY
	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                              END
	                           AND A.TRAN_TIME <= P_BASE_TIME
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A
	                     ,  MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B
	             , (SELECT A.CUSTOMER AS CUSTOMER
	                     , A.PACKAGE AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                             , B.MAT_GRP_3 AS PACKAGE
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1 
	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL
	                          FROM (SELECT A.FACTORY AS FACTORY
	                                     , A.MAT_ID AS MAT_ID
	                                     , A.OPER AS OPER
	                                     , A.LOSS_CODE AS LOSS_CODE
	                                     , SUM(A.LOSS_QTY_1) AS LOSS_QTY_MM_1 
	                                  FROM RSUMLOTLSD A 
	                                 WHERE A.FACTORY = P_FACTORY
	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), V_MONTH), 'YYYYMM')
	                                   AND A.OPER LIKE P_OPER || '%'
	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                             , MESMGR.MWIPMATDEF@RPTTOMES B
	                         WHERE A.FACTORY = B.FACTORY
	                           AND A.MAT_ID = B.MAT_ID
	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	                     , (SELECT KEY_1 AS LOSS_CODE
	                             , DATA_1 AS LOSS_DESC
	                             , DATA_5 AS LOSS_OPER_DESC
	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES
	                         WHERE FACTORY = P_FACTORY
	                           AND TABLE_NAME = 'LOSS_CODE'
	                           AND DATA_1 <> 'ETC'
	                           AND DATA_2 <> 'Y') B
	                 WHERE A.LOSS_CODE = B.LOSS_CODE
	                   AND A.LOSS_LEVEL <= V_DISPLAY_COUNT) C
	         WHERE A.CUSTOMER = B.CUSTOMER
	           AND A.PACKAGE = B.PACKAGE
	           AND A.OPER = B.OPER
	           AND A.CUSTOMER = C.CUSTOMER
	           AND A.PACKAGE = C.PACKAGE
	           AND A.OPER = C.OPER
	           AND A.LOSS_CODE = C.LOSS_CODE
	        UNION ALL
			SELECT A.CUSTOMER AS CUSTOMER
	             , A.PACKAGE AS PACKAGE
	             , A.OPER AS OPER
	             , A.LOSS_CODE AS LOSS_CODE
	             , 0 AS LOSS_PPM_MM_1
	             , 0 AS LOSS_PPM_MM_2
	             , 0 AS LOSS_PPM_MM_3
	             , 0 AS LOSS_PPM_WW_1
	             , 0 AS LOSS_PPM_WW_2
	             , 0 AS LOSS_PPM_WW_3
	             , 0 AS LOSS_PPM_WW_4
	             , CASE WHEN A.LOSS_QTY_DD_1 > 0 AND B.START_QTY_DD_1 > 0 THEN ROUND((A.LOSS_QTY_DD_1 * 100) / B.START_QTY_DD_1, 2) ELSE 0 END AS LOSS_PPM_DD_1
	             , CASE WHEN A.LOSS_QTY_DD_2 > 0 AND B.START_QTY_DD_2 > 0 THEN ROUND((A.LOSS_QTY_DD_2 * 100) / B.START_QTY_DD_2, 2) ELSE 0 END AS LOSS_PPM_DD_2
	             , CASE WHEN A.LOSS_QTY_DD_3 > 0 AND B.START_QTY_DD_3 > 0 THEN ROUND((A.LOSS_QTY_DD_3 * 100) / B.START_QTY_DD_3, 2) ELSE 0 END AS LOSS_PPM_DD_3
	             , CASE WHEN A.LOSS_QTY_DD_4 > 0 AND B.START_QTY_DD_4 > 0 THEN ROUND((A.LOSS_QTY_DD_4 * 100) / B.START_QTY_DD_4, 2) ELSE 0 END AS LOSS_PPM_DD_4
	             , CASE WHEN A.LOSS_QTY_DD_5 > 0 AND B.START_QTY_DD_5 > 0 THEN ROUND((A.LOSS_QTY_DD_5 * 100) / B.START_QTY_DD_5, 2) ELSE 0 END AS LOSS_PPM_DD_5
	             , CASE WHEN A.LOSS_QTY_DD_6 > 0 AND B.START_QTY_DD_6 > 0 THEN ROUND((A.LOSS_QTY_DD_6 * 100) / B.START_QTY_DD_6, 2) ELSE 0 END AS LOSS_PPM_DD_6
	             , CASE WHEN A.LOSS_QTY_DD_7 > 0 AND B.START_QTY_DD_7 > 0 THEN ROUND((A.LOSS_QTY_DD_7 * 100) / B.START_QTY_DD_7, 2) ELSE 0 END AS LOSS_PPM_DD_7
	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                     , SUM(A.LOSS_QTY_DD_1) AS LOSS_QTY_DD_1
	                     , SUM(A.LOSS_QTY_DD_2) AS LOSS_QTY_DD_2
	                     , SUM(A.LOSS_QTY_DD_3) AS LOSS_QTY_DD_3
	                     , SUM(A.LOSS_QTY_DD_4) AS LOSS_QTY_DD_4
	                     , SUM(A.LOSS_QTY_DD_5) AS LOSS_QTY_DD_5
	                     , SUM(A.LOSS_QTY_DD_6) AS LOSS_QTY_DD_6
	                     , SUM(A.LOSS_QTY_DD_7) AS LOSS_QTY_DD_7
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_1 
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_2
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_3
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_4
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_5
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_6
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.LOSS_QTY_1 ELSE 0 END) AS LOSS_QTY_DD_7
	                          FROM RSUMLOTLSD A 
	                         WHERE A.FACTORY = P_FACTORY
	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                              END
	                           AND A.TRAN_TIME <= P_BASE_TIME
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                     , MESMGR.MWIPMATDEF@RPTTOMES B
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	             , (SELECT B.MAT_GRP_1 AS CUSTOMER
	                     , B.MAT_GRP_3 AS PACKAGE
	                     , A.OPER AS OPER
	                     , SUM(A.START_QTY_DD_1) AS START_QTY_DD_1
	                     , SUM(A.START_QTY_DD_2) AS START_QTY_DD_2
	                     , SUM(A.START_QTY_DD_3) AS START_QTY_DD_3
	                     , SUM(A.START_QTY_DD_4) AS START_QTY_DD_4
	                     , SUM(A.START_QTY_DD_5) AS START_QTY_DD_5
	                     , SUM(A.START_QTY_DD_6) AS START_QTY_DD_6
	                     , SUM(A.START_QTY_DD_7) AS START_QTY_DD_7
	                  FROM (SELECT A.FACTORY AS FACTORY
	                             , A.MAT_ID AS MAT_ID
	                             , A.OPER AS OPER
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_1 
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_2
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_3
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_4
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_5
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_6
	                             , SUM(CASE TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.START_QTY_1 ELSE 0 END) AS START_QTY_DD_7
	                          FROM RSUMLOTLSM A 
	                         WHERE A.FACTORY = P_FACTORY
	                           AND A.TRAN_TIME >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                   WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                   WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                   WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                   WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                   WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                   WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                              END
	                           AND A.TRAN_TIME <= P_BASE_TIME
	                           AND A.OPER LIKE P_OPER || '%'
	                         GROUP BY A.FACTORY, A.MAT_ID, A.OPER) A
	                     , MESMGR.MWIPMATDEF@RPTTOMES B
	                 WHERE A.FACTORY = B.FACTORY
	                   AND A.MAT_ID = B.MAT_ID
	                   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                   AND B.MAT_GRP_3 LIKE P_PACKAGE || '%'
	                 GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER) B
	             , (SELECT A.CUSTOMER AS CUSTOMER
	                     , A.PACKAGE AS PACKAGE
	                     , A.OPER AS OPER
	                     , A.LOSS_CODE AS LOSS_CODE
	                  FROM (SELECT B.MAT_GRP_1 AS CUSTOMER
	                             , B.MAT_GRP_3 AS PACKAGE
	                             , A.OPER AS OPER
	                             , A.LOSS_CODE AS LOSS_CODE
	                             , SUM(A.LOSS_QTY_MM_1) AS LOSS_QTY_MM_1 
	                             , ROW_NUMBER() OVER(PARTITION BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER ORDER BY SUM(A.LOSS_QTY_MM_1) DESC) AS LOSS_LEVEL
	                          FROM (SELECT A.FACTORY AS FACTORY
	                                     , A.MAT_ID AS MAT_ID
	                                     , A.OPER AS OPER
	                                     , A.LOSS_CODE AS LOSS_CODE
	                                     , SUM(A.LOSS_QTY_1) AS LOSS_QTY_MM_1 
	                                  FROM RSUMLOTLSD A 
	                                 WHERE A.FACTORY = P_FACTORY
	                                   AND A.TRAN_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), V_MONTH), 'YYYYMM')
	                                   AND A.OPER LIKE P_OPER || '%'
	                                 GROUP BY A.FACTORY, A.MAT_ID, A.OPER, A.LOSS_CODE) A
	                             , MESMGR.MWIPMATDEF@RPTTOMES B
	                         WHERE A.FACTORY = B.FACTORY
	                           AND A.MAT_ID = B.MAT_ID
	                         GROUP BY B.MAT_GRP_1, B.MAT_GRP_3, A.OPER, A.LOSS_CODE) A
	                     , (SELECT KEY_1 AS LOSS_CODE
	                             , DATA_1 AS LOSS_DESC
	                             , DATA_5 AS LOSS_OPER_DESC
	                          FROM MESMGR.MGCMTBLDAT@RPTTOMES
	                         WHERE FACTORY = P_FACTORY
	                           AND TABLE_NAME = 'LOSS_CODE'
	                           AND DATA_1 <> 'ETC'
	                           AND DATA_2 <> 'Y') B
	                 WHERE A.LOSS_CODE = B.LOSS_CODE
	                   AND A.LOSS_LEVEL <= V_DISPLAY_COUNT) C
	         WHERE A.CUSTOMER = B.CUSTOMER
	           AND A.PACKAGE = B.PACKAGE
	           AND A.OPER = B.OPER
	           AND A.CUSTOMER = C.CUSTOMER
	           AND A.PACKAGE = C.PACKAGE
	           AND A.OPER = C.OPER
	           AND A.LOSS_CODE = C.LOSS_CODE) A
		 , (SELECT KEY_1 AS LOSS_CODE
	             , DATA_1 AS LOSS_DESC
	             , DATA_5 AS LOSS_OPER_DESC
	          FROM MESMGR.MGCMTBLDAT@RPTTOMES
	         WHERE FACTORY = P_FACTORY
	           AND TABLE_NAME = 'LOSS_CODE'
	           AND DATA_1 <> 'ETC'
	           AND DATA_2 <> 'Y') B
	 WHERE A.LOSS_CODE = B.LOSS_CODE 
	 GROUP BY A.CUSTOMER, A.PACKAGE, A.OPER, A.LOSS_CODE || ' ' || B.LOSS_DESC
	 ORDER BY A.CUSTOMER, A.PACKAGE, A.OPER, A.LOSS_CODE || ' ' || B.LOSS_DESC;

	PACKAGE_RESULT := PROCEDURE_RESULT;
	
EXCEPTION
    WHEN OTHERS THEN
	ROLLBACK;
END;


/************************************************************************************************************************************************************************************/
/************************************************************************************************************************************************************************************/


END PACKAGE_PQC030115;
/
