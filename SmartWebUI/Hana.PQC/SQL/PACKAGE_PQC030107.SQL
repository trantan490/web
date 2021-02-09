CREATE OR REPLACE PACKAGE RPTMGR.PACKAGE_PQC030107 IS
	/****************************************************
	 * COMMENT : 공정검사 품질불량현황을 조회한다..
	 *								
	 * CREATED BY : BEE-JAE JUNG(2010-06-29-화요일)
	 *											
	 * MODIFIED BY : BEE-JAE JUNG(2010-06-29-화요일)
	 ****************************************************/

	TYPE PKGCUR IS REF CURSOR;

	-- 01. 공정검사 품질불량현황 고객사별 PPM을 조회한다.
	PROCEDURE PROC_PQC030107_CUSTOMER_PPM
	(
		P_FACTORY		IN		VARCHAR2,			-- 공장구분
		P_BASE_MONTH	IN		VARCHAR2, 			-- 기준년월
		P_BASE_TIME		IN		VARCHAR2, 			-- 기준일자
		P_QC_OPER		IN		VARCHAR2,			-- 검사공정
		P_QC_TYPE		IN		VARCHAR2,			-- 검사목적
		P_CUSTOMER		IN		VARCHAR2,			-- 고객사구분
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
	);

	-- 02. 공정검사 품질불량현황 고객사별 LRR을 조회한다.
	PROCEDURE PROC_PQC030107_CUSTOMER_LRR
	(
		P_FACTORY		IN		VARCHAR2,			-- 공장구분
		P_BASE_MONTH	IN		VARCHAR2, 			-- 기준년월
		P_BASE_TIME		IN		VARCHAR2, 			-- 기준일자
		P_QC_OPER		IN		VARCHAR2,			-- 검사공정
		P_QC_TYPE		IN		VARCHAR2,			-- 검사목적
		P_CUSTOMER		IN		VARCHAR2,			-- 고객사구분
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
	);

	-- 03. 공정검사 품질불량현황 고객사별 YIELD을 조회한다.
	PROCEDURE PROC_PQC030107_CUSTOMER_YIELD
	(
		P_FACTORY		IN		VARCHAR2,			-- 공장구분
		P_BASE_MONTH	IN		VARCHAR2, 			-- 기준년월
		P_BASE_TIME		IN		VARCHAR2, 			-- 기준일자
		P_QC_OPER		IN		VARCHAR2,			-- 검사공정
		P_QC_TYPE		IN		VARCHAR2,			-- 검사목적
		P_CUSTOMER		IN		VARCHAR2,			-- 고객사구분
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
	);

	-- 04. 공정검사 품질불량현황 주간별 불량현황을 조회한다.
	PROCEDURE PROC_PQC030107_WEEK
	(
		P_FACTORY		IN		VARCHAR2,			-- 공장구분
		P_BASE_MONTH	IN		VARCHAR2, 			-- 기준년월
		P_BASE_TIME		IN		VARCHAR2, 			-- 기준일자
		P_QC_OPER		IN		VARCHAR2,			-- 검사공정
		P_QC_TYPE		IN		VARCHAR2,			-- 검사목적
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
	);
	
	-- 05. 공정검사 품질불량현황 일자별 불량현황을 조회한다.
	PROCEDURE PROC_PQC030107_DAYS
	(
		P_FACTORY		IN		VARCHAR2,			-- 공장구분
		P_BASE_MONTH	IN		VARCHAR2, 			-- 기준년월
		P_BASE_TIME		IN		VARCHAR2, 			-- 기준일자
		P_QC_OPER		IN		VARCHAR2,			-- 검사공정
		P_QC_TYPE		IN		VARCHAR2,			-- 검사목적
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
	);
	
END PACKAGE_PQC030107;


/************************************************************************************************************************************************************************************/
/************************************************************************************************************************************************************************************/


CREATE OR REPLACE PACKAGE BODY RPTMGR.PACKAGE_PQC030107 
AS

PROCEDURE PROC_PQC030107_CUSTOMER_PPM
(
		P_FACTORY		IN		VARCHAR2,			-- 공장구분
		P_BASE_MONTH	IN		VARCHAR2, 			-- 기준년월
		P_BASE_TIME		IN		VARCHAR2, 			-- 기준일자
		P_QC_OPER		IN		VARCHAR2,			-- 검사공정
		P_QC_TYPE		IN		VARCHAR2,			-- 검사목적
		P_CUSTOMER		IN		VARCHAR2,			-- 고객사구분
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
)
/************************************************************
 * COMMENT : 공정검사 품질불량현황 고객사별 PPM을 조회한다.
 *								
 * CREATED BY : BEE-JAE JUNG(2010-06-29-화요일)
 *											
 * MODIFIED BY : BEE-JAE JUNG(2010-06-29-화요일)
 ************************************************************/
 
/************************************************************************************************************
 COMMENT : REF CURDOR로 RETURN되는 SQL문을 TOAD, ORANGE에서 실행결과 확인법
 
 BEGIN
     PACKAGE_PQC030107.PROC_PQC030107_CUSTOMER_PPM('HMKA1', '201008', '20100831', '%', '%', '%', :CURSOR_OUT);
 END;
 ************************************************************************************************************/
IS
	-- 지역 변수 선언	
	PROCEDURE_RESULT	PKGCUR;
BEGIN

	OPEN PROCEDURE_RESULT FOR
	SELECT A.CUSTOMER AS CUSTOMER
		 , SUM(A.DEFECT_PPM_MM_1) AS DEFECT_PPM_MM_1
		 , SUM(A.DEFECT_PPM_MM_2) AS DEFECT_PPM_MM_2
		 , SUM(A.DEFECT_PPM_MM_3) AS DEFECT_PPM_MM_3
		 , SUM(A.DEFECT_PPM_WW_1) AS DEFECT_PPM_WW_1
		 , SUM(A.DEFECT_PPM_WW_2) AS DEFECT_PPM_WW_2
		 , SUM(A.DEFECT_PPM_WW_3) AS DEFECT_PPM_WW_3
		 , SUM(A.DEFECT_PPM_WW_4) AS DEFECT_PPM_WW_4
		 , SUM(A.DEFECT_PPM_DD_1) AS DEFECT_PPM_DD_1
		 , SUM(A.DEFECT_PPM_DD_2) AS DEFECT_PPM_DD_2
		 , SUM(A.DEFECT_PPM_DD_3) AS DEFECT_PPM_DD_3
		 , SUM(A.DEFECT_PPM_DD_4) AS DEFECT_PPM_DD_4
		 , SUM(A.DEFECT_PPM_DD_5) AS DEFECT_PPM_DD_5
		 , SUM(A.DEFECT_PPM_DD_6) AS DEFECT_PPM_DD_6
		 , SUM(A.DEFECT_PPM_DD_7) AS DEFECT_PPM_DD_7
	  FROM (SELECT A.CUSTOMER AS CUSTOMER
	             , CASE WHEN A.DEFECT_QTY_MM_1 > 0 AND B.SAMPLE_QTY_MM_1 > 0 THEN ROUND((A.DEFECT_QTY_MM_1 * 1000000) / B.SAMPLE_QTY_MM_1, 2) ELSE 0 END AS DEFECT_PPM_MM_1 
	             , CASE WHEN A.DEFECT_QTY_MM_2 > 0 AND B.SAMPLE_QTY_MM_2 > 0 THEN ROUND((A.DEFECT_QTY_MM_2 * 1000000) / B.SAMPLE_QTY_MM_2, 2) ELSE 0 END AS DEFECT_PPM_MM_2
	             , CASE WHEN A.DEFECT_QTY_MM_3 > 0 AND B.SAMPLE_QTY_MM_3 > 0 THEN ROUND((A.DEFECT_QTY_MM_3 * 1000000) / B.SAMPLE_QTY_MM_3, 2) ELSE 0 END AS DEFECT_PPM_MM_3
	             , 0 AS DEFECT_PPM_WW_1
	             , 0 AS DEFECT_PPM_WW_2
	             , 0 AS DEFECT_PPM_WW_3
	             , 0 AS DEFECT_PPM_WW_4
	             , 0 AS DEFECT_PPM_DD_1
	             , 0 AS DEFECT_PPM_DD_2
	             , 0 AS DEFECT_PPM_DD_3
	             , 0 AS DEFECT_PPM_DD_4
	             , 0 AS DEFECT_PPM_DD_5
	             , 0 AS DEFECT_PPM_DD_6
	             , 0 AS DEFECT_PPM_DD_7
	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_1 
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -1), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_2
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -0), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_3
	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM')
	                   AND A.WORK_MONTH <= P_BASE_MONTH
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%'
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) A
	             , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_1 
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -1), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_2
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -0), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_3
	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM')
	                   AND A.WORK_MONTH <= P_BASE_MONTH
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%' 
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) B
	         WHERE A.CUSTOMER = B.CUSTOMER
			UNION ALL
			SELECT A.CUSTOMER AS CUSTOMER
	             , 0 AS DEFECT_PPM_MM_1 
	             , 0 AS DEFECT_PPM_MM_2
	             , 0 AS DEFECT_PPM_MM_3
	             , CASE WHEN A.DEFECT_QTY_WW_1 > 0 AND B.SAMPLE_QTY_WW_1 > 0 THEN ROUND((A.DEFECT_QTY_WW_1 * 1000000) / B.SAMPLE_QTY_WW_1, 2) ELSE 0 END AS DEFECT_PPM_WW_1
	             , CASE WHEN A.DEFECT_QTY_WW_2 > 0 AND B.SAMPLE_QTY_WW_2 > 0 THEN ROUND((A.DEFECT_QTY_WW_2 * 1000000) / B.SAMPLE_QTY_WW_2, 2) ELSE 0 END AS DEFECT_PPM_WW_2
	             , CASE WHEN A.DEFECT_QTY_WW_3 > 0 AND B.SAMPLE_QTY_WW_3 > 0 THEN ROUND((A.DEFECT_QTY_WW_3 * 1000000) / B.SAMPLE_QTY_WW_3, 2) ELSE 0 END AS DEFECT_PPM_WW_3
	             , CASE WHEN A.DEFECT_QTY_WW_4 > 0 AND B.SAMPLE_QTY_WW_4 > 0 THEN ROUND((A.DEFECT_QTY_WW_4 * 1000000) / B.SAMPLE_QTY_WW_4, 2) ELSE 0 END AS DEFECT_PPM_WW_4
	             , 0 AS DEFECT_PPM_DD_1
	             , 0 AS DEFECT_PPM_DD_2
	             , 0 AS DEFECT_PPM_DD_3
	             , 0 AS DEFECT_PPM_DD_4
	             , 0 AS DEFECT_PPM_DD_5
	             , 0 AS DEFECT_PPM_DD_6
	             , 0 AS DEFECT_PPM_DD_7
	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_1
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_2
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_3
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 6, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 5, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 4, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 3, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 2, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 1, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 0, 'YYYYMMDD')
	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_4
	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                      END
	                   AND A.WORK_DATE <= P_BASE_TIME
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%' 
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) A
	             , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_1
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_2
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_3
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 6, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 5, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 4, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 3, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 2, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 1, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 0, 'YYYYMMDD')
	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_4
	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                      END
	                   AND A.WORK_DATE <= P_BASE_TIME
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%' 
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) B
	         WHERE A.CUSTOMER = B.CUSTOMER
			UNION ALL
			SELECT A.CUSTOMER AS CUSTOMER
	             , 0 AS DEFECT_PPM_MM_1 
	             , 0 AS DEFECT_PPM_MM_2
	             , 0 AS DEFECT_PPM_MM_3
	             , 0 AS DEFECT_PPM_WW_1
	             , 0 AS DEFECT_PPM_WW_2
	             , 0 AS DEFECT_PPM_WW_3
	             , 0 AS DEFECT_PPM_WW_4
	             , CASE WHEN A.DEFECT_QTY_DD_1 > 0 AND B.SAMPLE_QTY_DD_1 > 0 THEN ROUND((A.DEFECT_QTY_DD_1 * 1000000) / B.SAMPLE_QTY_DD_1, 2) ELSE 0 END AS DEFECT_PPM_DD_1
	             , CASE WHEN A.DEFECT_QTY_DD_2 > 0 AND B.SAMPLE_QTY_DD_2 > 0 THEN ROUND((A.DEFECT_QTY_DD_2 * 1000000) / B.SAMPLE_QTY_DD_2, 2) ELSE 0 END AS DEFECT_PPM_DD_2
	             , CASE WHEN A.DEFECT_QTY_DD_3 > 0 AND B.SAMPLE_QTY_DD_3 > 0 THEN ROUND((A.DEFECT_QTY_DD_3 * 1000000) / B.SAMPLE_QTY_DD_3, 2) ELSE 0 END AS DEFECT_PPM_DD_3
	             , CASE WHEN A.DEFECT_QTY_DD_4 > 0 AND B.SAMPLE_QTY_DD_4 > 0 THEN ROUND((A.DEFECT_QTY_DD_4 * 1000000) / B.SAMPLE_QTY_DD_4, 2) ELSE 0 END AS DEFECT_PPM_DD_4
	             , CASE WHEN A.DEFECT_QTY_DD_5 > 0 AND B.SAMPLE_QTY_DD_5 > 0 THEN ROUND((A.DEFECT_QTY_DD_5 * 1000000) / B.SAMPLE_QTY_DD_5, 2) ELSE 0 END AS DEFECT_PPM_DD_5
	             , CASE WHEN A.DEFECT_QTY_DD_6 > 0 AND B.SAMPLE_QTY_DD_6 > 0 THEN ROUND((A.DEFECT_QTY_DD_6 * 1000000) / B.SAMPLE_QTY_DD_6, 2) ELSE 0 END AS DEFECT_PPM_DD_6
	             , CASE WHEN A.DEFECT_QTY_DD_7 > 0 AND B.SAMPLE_QTY_DD_7 > 0 THEN ROUND((A.DEFECT_QTY_DD_7 * 1000000) / B.SAMPLE_QTY_DD_7, 2) ELSE 0 END AS DEFECT_PPM_DD_7
	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_1 
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_2
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_3
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_4
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_5
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_6
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_7
	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                      END 
	                   AND A.WORK_DATE <= P_BASE_TIME
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%' 
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) A
	             , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_1 
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_2
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_3
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_4
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_5
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_6
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_7
	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                      END 
	                   AND A.WORK_DATE <= P_BASE_TIME
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%' 
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) B
	         WHERE A.CUSTOMER = B.CUSTOMER) A
	 GROUP BY A.CUSTOMER 
	 ORDER BY DECODE(A.CUSTOMER, 'SE', 0, 'HX', 1, 2); 

	PACKAGE_RESULT := PROCEDURE_RESULT;
	
EXCEPTION
    WHEN OTHERS THEN
	ROLLBACK;
END;


/************************************************************************************************************************************************************************************/
/************************************************************************************************************************************************************************************/


PROCEDURE PROC_PQC030107_CUSTOMER_LRR
(
		P_FACTORY		IN		VARCHAR2,			-- 공장구분
		P_BASE_MONTH	IN		VARCHAR2, 			-- 기준년월
		P_BASE_TIME		IN		VARCHAR2, 			-- 기준일자
		P_QC_OPER		IN		VARCHAR2,			-- 검사공정
		P_QC_TYPE		IN		VARCHAR2,			-- 검사목적
		P_CUSTOMER		IN		VARCHAR2,			-- 고객사구분
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
)
/************************************************************
 * COMMENT : 공정검사 품질불량현황 고객사별 LRR을 조회한다.
 *								
 * CREATED BY : BEE-JAE JUNG(2010-06-29-화요일)
 *											
 * MODIFIED BY : BEE-JAE JUNG(2010-06-29-화요일)
 ************************************************************/
 
/************************************************************************************************************
 COMMENT : REF CURDOR로 RETURN되는 SQL문을 TOAD, ORANGE에서 실행결과 확인법
 
 BEGIN
     PACKAGE_PQC030107.PROC_PQC030107_CUSTOMER_LRR('HMKA1', '201008', '20100831', '%', '%', '%', :CURSOR_OUT);
 END;
 ************************************************************************************************************/
IS
	-- 지역 변수 선언	
	PROCEDURE_RESULT	PKGCUR;
BEGIN


	OPEN PROCEDURE_RESULT FOR
	SELECT A.CUSTOMER AS CUSTOMER
		 , SUM(A.DEFECT_LRR_MM_1) AS DEFECT_LRR_MM_1
		 , SUM(A.DEFECT_LRR_MM_2) AS DEFECT_LRR_MM_2
		 , SUM(A.DEFECT_LRR_MM_3) AS DEFECT_LRR_MM_3
		 , SUM(A.DEFECT_LRR_WW_1) AS DEFECT_LRR_WW_1
		 , SUM(A.DEFECT_LRR_WW_2) AS DEFECT_LRR_WW_2
		 , SUM(A.DEFECT_LRR_WW_3) AS DEFECT_LRR_WW_3
		 , SUM(A.DEFECT_LRR_WW_4) AS DEFECT_LRR_WW_4
		 , SUM(A.DEFECT_LRR_DD_1) AS DEFECT_LRR_DD_1
		 , SUM(A.DEFECT_LRR_DD_2) AS DEFECT_LRR_DD_2
		 , SUM(A.DEFECT_LRR_DD_3) AS DEFECT_LRR_DD_3
		 , SUM(A.DEFECT_LRR_DD_4) AS DEFECT_LRR_DD_4
		 , SUM(A.DEFECT_LRR_DD_5) AS DEFECT_LRR_DD_5
		 , SUM(A.DEFECT_LRR_DD_6) AS DEFECT_LRR_DD_6
		 , SUM(A.DEFECT_LRR_DD_7) AS DEFECT_LRR_DD_7
	  FROM (SELECT A.CUSTOMER AS CUSTOMER
	             , CASE WHEN A.DEFECT_QTY_MM_1 > 0 AND B.SAMPLE_QTY_MM_1 > 0 THEN ROUND((A.DEFECT_QTY_MM_1 * 100) / B.SAMPLE_QTY_MM_1, 2) ELSE 0 END AS DEFECT_LRR_MM_1 
	             , CASE WHEN A.DEFECT_QTY_MM_2 > 0 AND B.SAMPLE_QTY_MM_2 > 0 THEN ROUND((A.DEFECT_QTY_MM_2 * 100) / B.SAMPLE_QTY_MM_2, 2) ELSE 0 END AS DEFECT_LRR_MM_2
	             , CASE WHEN A.DEFECT_QTY_MM_3 > 0 AND B.SAMPLE_QTY_MM_3 > 0 THEN ROUND((A.DEFECT_QTY_MM_3 * 100) / B.SAMPLE_QTY_MM_3, 2) ELSE 0 END AS DEFECT_LRR_MM_3
	             , 0 AS DEFECT_LRR_WW_1
	             , 0 AS DEFECT_LRR_WW_2
	             , 0 AS DEFECT_LRR_WW_3
	             , 0 AS DEFECT_LRR_WW_4
	             , 0 AS DEFECT_LRR_DD_1
	             , 0 AS DEFECT_LRR_DD_2
	             , 0 AS DEFECT_LRR_DD_3
	             , 0 AS DEFECT_LRR_DD_4
	             , 0 AS DEFECT_LRR_DD_5
	             , 0 AS DEFECT_LRR_DD_6
	             , 0 AS DEFECT_LRR_DD_7
	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_1 
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -1), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_2
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -0), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_3
	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM')
	                   AND A.WORK_MONTH <= P_BASE_MONTH
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%'
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%'
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) A
	             , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_1 
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -1), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_2
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -0), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_3
	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM')
	                   AND A.WORK_MONTH <= P_BASE_MONTH
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%'
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%' 
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) B
	         WHERE A.CUSTOMER = B.CUSTOMER
			UNION ALL
			SELECT A.CUSTOMER AS CUSTOMER
	             , 0 AS DEFECT_LRR_MM_1 
	             , 0 AS DEFECT_LRR_MM_2
	             , 0 AS DEFECT_LRR_MM_3
	             , CASE WHEN A.DEFECT_QTY_WW_1 > 0 AND B.SAMPLE_QTY_WW_1 > 0 THEN ROUND((A.DEFECT_QTY_WW_1 * 100) / B.SAMPLE_QTY_WW_1, 2) ELSE 0 END AS DEFECT_LRR_WW_1
	             , CASE WHEN A.DEFECT_QTY_WW_2 > 0 AND B.SAMPLE_QTY_WW_2 > 0 THEN ROUND((A.DEFECT_QTY_WW_2 * 100) / B.SAMPLE_QTY_WW_2, 2) ELSE 0 END AS DEFECT_LRR_WW_2
	             , CASE WHEN A.DEFECT_QTY_WW_3 > 0 AND B.SAMPLE_QTY_WW_3 > 0 THEN ROUND((A.DEFECT_QTY_WW_3 * 100) / B.SAMPLE_QTY_WW_3, 2) ELSE 0 END AS DEFECT_LRR_WW_3
	             , CASE WHEN A.DEFECT_QTY_WW_4 > 0 AND B.SAMPLE_QTY_WW_4 > 0 THEN ROUND((A.DEFECT_QTY_WW_4 * 100) / B.SAMPLE_QTY_WW_4, 2) ELSE 0 END AS DEFECT_LRR_WW_4
	             , 0 AS DEFECT_LRR_DD_1
	             , 0 AS DEFECT_LRR_DD_2
	             , 0 AS DEFECT_LRR_DD_3
	             , 0 AS DEFECT_LRR_DD_4
	             , 0 AS DEFECT_LRR_DD_5
	             , 0 AS DEFECT_LRR_DD_6
	             , 0 AS DEFECT_LRR_DD_7
	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_1
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_2
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_3
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 6, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 5, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 4, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 3, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 2, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 1, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 0, 'YYYYMMDD')
	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_4
	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                      END
	                   AND A.WORK_DATE <= P_BASE_TIME
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%'
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%' 
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) A
	             , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_1
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_2
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_3
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 6, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 5, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 4, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 3, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 2, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 1, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 0, 'YYYYMMDD')
	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_4
	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                      END
	                   AND A.WORK_DATE <= P_BASE_TIME
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%'
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%' 
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) B
	         WHERE A.CUSTOMER = B.CUSTOMER
			UNION ALL
			SELECT A.CUSTOMER AS CUSTOMER
	             , 0 AS DEFECT_LRR_MM_1 
	             , 0 AS DEFECT_LRR_MM_2
	             , 0 AS DEFECT_LRR_MM_3
	             , 0 AS DEFECT_LRR_WW_1
	             , 0 AS DEFECT_LRR_WW_2
	             , 0 AS DEFECT_LRR_WW_3
	             , 0 AS DEFECT_LRR_WW_4
	             , CASE WHEN A.DEFECT_QTY_DD_1 > 0 AND B.SAMPLE_QTY_DD_1 > 0 THEN ROUND((A.DEFECT_QTY_DD_1 * 100) / B.SAMPLE_QTY_DD_1, 2) ELSE 0 END AS DEFECT_LRR_DD_1
	             , CASE WHEN A.DEFECT_QTY_DD_2 > 0 AND B.SAMPLE_QTY_DD_2 > 0 THEN ROUND((A.DEFECT_QTY_DD_2 * 100) / B.SAMPLE_QTY_DD_2, 2) ELSE 0 END AS DEFECT_LRR_DD_2
	             , CASE WHEN A.DEFECT_QTY_DD_3 > 0 AND B.SAMPLE_QTY_DD_3 > 0 THEN ROUND((A.DEFECT_QTY_DD_3 * 100) / B.SAMPLE_QTY_DD_3, 2) ELSE 0 END AS DEFECT_LRR_DD_3
	             , CASE WHEN A.DEFECT_QTY_DD_4 > 0 AND B.SAMPLE_QTY_DD_4 > 0 THEN ROUND((A.DEFECT_QTY_DD_4 * 100) / B.SAMPLE_QTY_DD_4, 2) ELSE 0 END AS DEFECT_LRR_DD_4
	             , CASE WHEN A.DEFECT_QTY_DD_5 > 0 AND B.SAMPLE_QTY_DD_5 > 0 THEN ROUND((A.DEFECT_QTY_DD_5 * 100) / B.SAMPLE_QTY_DD_5, 2) ELSE 0 END AS DEFECT_LRR_DD_5
	             , CASE WHEN A.DEFECT_QTY_DD_6 > 0 AND B.SAMPLE_QTY_DD_6 > 0 THEN ROUND((A.DEFECT_QTY_DD_6 * 100) / B.SAMPLE_QTY_DD_6, 2) ELSE 0 END AS DEFECT_LRR_DD_6
	             , CASE WHEN A.DEFECT_QTY_DD_7 > 0 AND B.SAMPLE_QTY_DD_7 > 0 THEN ROUND((A.DEFECT_QTY_DD_7 * 100) / B.SAMPLE_QTY_DD_7, 2) ELSE 0 END AS DEFECT_LRR_DD_7
	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_1 
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_2
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_3
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_4
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_5
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_6
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_7
	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                      END 
	                   AND A.WORK_DATE <= P_BASE_TIME
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%'
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%' 
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) A
	             , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_1 
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_2
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_3
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_4
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_5
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_6
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_7
	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                      END 
	                   AND A.WORK_DATE <= P_BASE_TIME
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%'
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%' 
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) B
	         WHERE A.CUSTOMER = B.CUSTOMER) A
	 GROUP BY A.CUSTOMER 
	 ORDER BY DECODE(A.CUSTOMER, 'SE', 0, 'HX', 1, 2); 

	PACKAGE_RESULT := PROCEDURE_RESULT;
	
EXCEPTION
    WHEN OTHERS THEN
	ROLLBACK;
END;


/************************************************************************************************************************************************************************************/
/************************************************************************************************************************************************************************************/


PROCEDURE PROC_PQC030107_CUSTOMER_YIELD
(
		P_FACTORY		IN		VARCHAR2,			-- 공장구분
		P_BASE_MONTH	IN		VARCHAR2, 			-- 기준년월
		P_BASE_TIME		IN		VARCHAR2, 			-- 기준일자
		P_QC_OPER		IN		VARCHAR2,			-- 검사공정
		P_QC_TYPE		IN		VARCHAR2,			-- 검사목적
		P_CUSTOMER		IN		VARCHAR2,			-- 고객사구분
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
)
/************************************************************
 * COMMENT : 공정검사 품질불량현황 고객사별 PPM을 조회한다.
 *								
 * CREATED BY : BEE-JAE JUNG(2010-06-29-화요일)
 *											
 * MODIFIED BY : BEE-JAE JUNG(2010-06-29-화요일)
 ************************************************************/
 
/************************************************************************************************************
 COMMENT : REF CURDOR로 RETURN되는 SQL문을 TOAD, ORANGE에서 실행결과 확인법
 
 BEGIN
     PACKAGE_PQC030107.PROC_PQC030107_CUSTOMER_YIELD('HMKA1', '201008', '20100831', '%', '%', '%', :CURSOR_OUT);
 END;
 ************************************************************************************************************/
IS
	-- 지역 변수 선언	
	PROCEDURE_RESULT	PKGCUR;
BEGIN


	OPEN PROCEDURE_RESULT FOR
	SELECT A.CUSTOMER AS CUSTOMER
		 , SUM(A.DEFECT_YIELD_MM_1) AS DEFECT_YIELD_MM_1
		 , SUM(A.DEFECT_YIELD_MM_2) AS DEFECT_YIELD_MM_2
		 , SUM(A.DEFECT_YIELD_MM_3) AS DEFECT_YIELD_MM_3
		 , SUM(A.DEFECT_YIELD_WW_1) AS DEFECT_YIELD_WW_1
		 , SUM(A.DEFECT_YIELD_WW_2) AS DEFECT_YIELD_WW_2
		 , SUM(A.DEFECT_YIELD_WW_3) AS DEFECT_YIELD_WW_3
		 , SUM(A.DEFECT_YIELD_WW_4) AS DEFECT_YIELD_WW_4
		 , SUM(A.DEFECT_YIELD_DD_1) AS DEFECT_YIELD_DD_1
		 , SUM(A.DEFECT_YIELD_DD_2) AS DEFECT_YIELD_DD_2
		 , SUM(A.DEFECT_YIELD_DD_3) AS DEFECT_YIELD_DD_3
		 , SUM(A.DEFECT_YIELD_DD_4) AS DEFECT_YIELD_DD_4
		 , SUM(A.DEFECT_YIELD_DD_5) AS DEFECT_YIELD_DD_5
		 , SUM(A.DEFECT_YIELD_DD_6) AS DEFECT_YIELD_DD_6
		 , SUM(A.DEFECT_YIELD_DD_7) AS DEFECT_YIELD_DD_7
	  FROM (SELECT A.CUSTOMER AS CUSTOMER
	             , CASE WHEN A.DEFECT_QTY_MM_1 > 0 AND B.SAMPLE_QTY_MM_1 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_MM_1 * 100) / B.SAMPLE_QTY_MM_1), 2) ELSE 0 END AS DEFECT_YIELD_MM_1 
	             , CASE WHEN A.DEFECT_QTY_MM_2 > 0 AND B.SAMPLE_QTY_MM_2 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_MM_2 * 100) / B.SAMPLE_QTY_MM_2), 2) ELSE 0 END AS DEFECT_YIELD_MM_2
	             , CASE WHEN A.DEFECT_QTY_MM_3 > 0 AND B.SAMPLE_QTY_MM_3 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_MM_3 * 100) / B.SAMPLE_QTY_MM_3), 2) ELSE 0 END AS DEFECT_YIELD_MM_3
	             , 0 AS DEFECT_YIELD_WW_1
	             , 0 AS DEFECT_YIELD_WW_2
	             , 0 AS DEFECT_YIELD_WW_3
	             , 0 AS DEFECT_YIELD_WW_4
	             , 0 AS DEFECT_YIELD_DD_1
	             , 0 AS DEFECT_YIELD_DD_2
	             , 0 AS DEFECT_YIELD_DD_3
	             , 0 AS DEFECT_YIELD_DD_4
	             , 0 AS DEFECT_YIELD_DD_5
	             , 0 AS DEFECT_YIELD_DD_6
	             , 0 AS DEFECT_YIELD_DD_7
	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_1 
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -1), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_2
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -0), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_3
	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM')
	                   AND A.WORK_MONTH <= P_BASE_MONTH
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%'
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%' 
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) A
	             , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_1 
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -1), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_2
	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -0), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_3
	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -2), 'YYYYMM')
	                   AND A.WORK_MONTH <= P_BASE_MONTH
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%'
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%' 
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) B
	         WHERE A.CUSTOMER = B.CUSTOMER
			UNION ALL
			SELECT A.CUSTOMER AS CUSTOMER
	             , 0 AS DEFECT_YIELD_MM_1 
	             , 0 AS DEFECT_YIELD_MM_2
	             , 0 AS DEFECT_YIELD_MM_3
	             , CASE WHEN A.DEFECT_QTY_WW_1 > 0 AND B.SAMPLE_QTY_WW_1 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_WW_1 * 100) / B.SAMPLE_QTY_WW_1), 2) ELSE 0 END AS DEFECT_YIELD_WW_1
	             , CASE WHEN A.DEFECT_QTY_WW_2 > 0 AND B.SAMPLE_QTY_WW_2 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_WW_2 * 100) / B.SAMPLE_QTY_WW_2), 2) ELSE 0 END AS DEFECT_YIELD_WW_2
	             , CASE WHEN A.DEFECT_QTY_WW_3 > 0 AND B.SAMPLE_QTY_WW_3 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_WW_3 * 100) / B.SAMPLE_QTY_WW_3), 2) ELSE 0 END AS DEFECT_YIELD_WW_3
	             , CASE WHEN A.DEFECT_QTY_WW_4 > 0 AND B.SAMPLE_QTY_WW_4 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_WW_4 * 100) / B.SAMPLE_QTY_WW_4), 2) ELSE 0 END AS DEFECT_YIELD_WW_4
	             , 0 AS DEFECT_YIELD_DD_1
	             , 0 AS DEFECT_YIELD_DD_2
	             , 0 AS DEFECT_YIELD_DD_3
	             , 0 AS DEFECT_YIELD_DD_4
	             , 0 AS DEFECT_YIELD_DD_5
	             , 0 AS DEFECT_YIELD_DD_6
	             , 0 AS DEFECT_YIELD_DD_7
	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_1
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_2
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_3
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 6, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 5, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 4, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 3, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 2, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 1, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 0, 'YYYYMMDD')
	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_4
	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                      END
	                   AND A.WORK_DATE <= P_BASE_TIME
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%'
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%' 
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) A
	             , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_1
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 15, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 16, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 17, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 18, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 19, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 20, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 14, 'YYYYMMDD')
	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_2
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 8, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 9, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 10, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 11, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 12, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 13, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 7, 'YYYYMMDD')
	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_3
	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                                    END
	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 6, 'YYYYMMDD')
	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 5, 'YYYYMMDD')
	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 4, 'YYYYMMDD')
	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 3, 'YYYYMMDD')
	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 2, 'YYYYMMDD')
	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 1, 'YYYYMMDD')
	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') + 0, 'YYYYMMDD')
	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_4
	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 21, 'YYYYMMDD')
	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 22, 'YYYYMMDD')
	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 23, 'YYYYMMDD')
	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 24, 'YYYYMMDD')
	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 25, 'YYYYMMDD')
	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 26, 'YYYYMMDD')
	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 27, 'YYYYMMDD')
	                                                      END
	                   AND A.WORK_DATE <= P_BASE_TIME
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%'
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%' 
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) B
	         WHERE A.CUSTOMER = B.CUSTOMER
			UNION ALL
			SELECT A.CUSTOMER AS CUSTOMER
	             , 0 AS DEFECT_YIELD_MM_1 
	             , 0 AS DEFECT_YIELD_MM_2
	             , 0 AS DEFECT_YIELD_MM_3
	             , 0 AS DEFECT_YIELD_WW_1
	             , 0 AS DEFECT_YIELD_WW_2
	             , 0 AS DEFECT_YIELD_WW_3
	             , 0 AS DEFECT_YIELD_WW_4
	             , CASE WHEN A.DEFECT_QTY_DD_1 > 0 AND B.SAMPLE_QTY_DD_1 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_DD_1 * 100) / B.SAMPLE_QTY_DD_1), 2) ELSE 0 END AS DEFECT_YIELD_DD_1
	             , CASE WHEN A.DEFECT_QTY_DD_2 > 0 AND B.SAMPLE_QTY_DD_2 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_DD_2 * 100) / B.SAMPLE_QTY_DD_2), 2) ELSE 0 END AS DEFECT_YIELD_DD_2
	             , CASE WHEN A.DEFECT_QTY_DD_3 > 0 AND B.SAMPLE_QTY_DD_3 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_DD_3 * 100) / B.SAMPLE_QTY_DD_3), 2) ELSE 0 END AS DEFECT_YIELD_DD_3
	             , CASE WHEN A.DEFECT_QTY_DD_4 > 0 AND B.SAMPLE_QTY_DD_4 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_DD_4 * 100) / B.SAMPLE_QTY_DD_4), 2) ELSE 0 END AS DEFECT_YIELD_DD_4
	             , CASE WHEN A.DEFECT_QTY_DD_5 > 0 AND B.SAMPLE_QTY_DD_5 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_DD_5 * 100) / B.SAMPLE_QTY_DD_5), 2) ELSE 0 END AS DEFECT_YIELD_DD_5
	             , CASE WHEN A.DEFECT_QTY_DD_6 > 0 AND B.SAMPLE_QTY_DD_6 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_DD_6 * 100) / B.SAMPLE_QTY_DD_6), 2) ELSE 0 END AS DEFECT_YIELD_DD_6
	             , CASE WHEN A.DEFECT_QTY_DD_7 > 0 AND B.SAMPLE_QTY_DD_7 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_DD_7 * 100) / B.SAMPLE_QTY_DD_7), 2) ELSE 0 END AS DEFECT_YIELD_DD_7
	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_1 
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_2
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_3
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_4
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_5
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_6
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_7
	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                      END 
	                   AND A.WORK_DATE <= P_BASE_TIME
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%'
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%' 
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) A
	             , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'
	                                        WHEN 'HX' THEN 'HX'
	                                        ELSE 'ETC'
	                       END AS CUSTOMER 
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_1 
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_2
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_3
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_4
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_5
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_6
	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_7
	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B 
	                 WHERE A.FACTORY = B.FACTORY 
	                   AND A.MAT_ID = B.MAT_ID 
	                   AND A.FACTORY = P_FACTORY
	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                      END 
	                   AND A.WORK_DATE <= P_BASE_TIME
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%'
					   AND B.MAT_GRP_1 LIKE P_CUSTOMER || '%' 
	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' 
	                                           WHEN 'HX' THEN 'HX' 
	                                           ELSE 'ETC' 
	                          END) B
	         WHERE A.CUSTOMER = B.CUSTOMER) A
	 GROUP BY A.CUSTOMER 
	 ORDER BY DECODE(A.CUSTOMER, 'SE', 0, 'HX', 1, 2); 

	PACKAGE_RESULT := PROCEDURE_RESULT;
	
EXCEPTION
    WHEN OTHERS THEN
	ROLLBACK;
END;


/************************************************************************************************************************************************************************************/
/************************************************************************************************************************************************************************************/


PROCEDURE PROC_PQC030107_WEEK
(
		P_FACTORY		IN		VARCHAR2,			-- 공장구분
		P_BASE_MONTH	IN		VARCHAR2, 			-- 기준년월
		P_BASE_TIME		IN		VARCHAR2, 			-- 기준일자
		P_QC_OPER		IN		VARCHAR2,			-- 검사공정
		P_QC_TYPE		IN		VARCHAR2,			-- 검사목적
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
)
/************************************************************
 * COMMENT : 공정검사 품질불량현황 주별 데이터를 조회한다.
 *								
 * CREATED BY : BEE-JAE JUNG(2010-06-29-화요일)
 *											
 * MODIFIED BY : BEE-JAE JUNG(2010-06-29-화요일)
 ************************************************************/
 
/************************************************************************************************************
 COMMENT : REF CURDOR로 RETURN되는 SQL문을 TOAD, ORANGE에서 실행결과 확인법
 
 BEGIN
     PACKAGE_PQC030107.PROC_PQC030107_WEEK('HMKA1', '201008', '20100831', '%', '%', :CURSOR_OUT);
 END;
 ************************************************************************************************************/
IS
	-- 지역 변수 선언	
	PROCEDURE_RESULT	PKGCUR;
	V_DISPLAY_COUNT		INT;
BEGIN
	
	-- 2010-06-29-정비재 : 항목별 표시할 데이터 건수를 지정한다.
	V_DISPLAY_COUNT := 5;

	OPEN PROCEDURE_RESULT FOR
	SELECT A.DEFECT_CODE_MM AS DEFECT_CODE_MM
		 , A.DEFECT_QTY_MM AS DEFECT_QTY_MM
		 , A.SAMPLE_QTY_MM AS SAMPLE_QTY_MM
		 , NVL(B.DEFECT_CODE_WW, ' ') AS DEFECT_CODE_WW
		 , NVL(B.DEFECT_QTY_WW, 0) AS DEFECT_QTY_WW
		 , NVL(B.SAMPLE_QTY_WW, 0) AS SAMPLE_QTY_WW
		 , NVL(C.DEFECT_CODE_DD, ' ') AS DEFECT_CODE_DD
		 , NVL(C.DEFECT_QTY_DD, 0) AS DEFECT_QTY_DD
		 , NVL(C.SAMPLE_QTY_DD, 0) AS SAMPLE_QTY_DD
	  FROM (SELECT A.ROW_LEVEL AS ROW_LEVEL
	             , A.DEFECT_CODE AS DEFECT_CODE_MM
	             , A.DEFECT_QTY_1 AS DEFECT_QTY_MM
	             , B.SAMPLE_QTY_1 AS SAMPLE_QTY_MM
	          FROM (SELECT A.DEFECT_CODE AS DEFECT_CODE
	                     , SUM(A.DEFECT_LOT_QTY_1) AS DEFECT_QTY_1
	                     , ROW_NUMBER() OVER(ORDER BY SUM(A.DEFECT_LOT_QTY_1) DESC) AS ROW_LEVEL
	                  FROM RSUMPQCDFT A
	                 WHERE A.FACTORY = P_FACTORY
	                   AND A.WORK_MONTH = P_BASE_MONTH
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%' 
	                 GROUP BY A.DEFECT_CODE) A
	             , (SELECT SUM(A.QC_LOT_QTY_1) AS SAMPLE_QTY_1
	                  FROM RSUMPQCLOT A
	                 WHERE A.FACTORY = P_FACTORY
	                   AND A.WORK_MONTH = P_BASE_MONTH
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%' ) B
	         WHERE A.ROW_LEVEL <= V_DISPLAY_COUNT) A
		 , (SELECT A.ROW_LEVEL AS ROW_LEVEL
	             , A.DEFECT_CODE AS DEFECT_CODE_WW
	             , A.DEFECT_QTY_1 AS DEFECT_QTY_WW
	             , B.SAMPLE_QTY_1 AS SAMPLE_QTY_WW
	          FROM (SELECT A.DEFECT_CODE AS DEFECT_CODE
	                     , SUM(A.DEFECT_LOT_QTY_1) AS DEFECT_QTY_1
	                     , ROW_NUMBER() OVER(ORDER BY SUM(A.DEFECT_LOT_QTY_1) DESC) AS ROW_LEVEL
	                  FROM RSUMPQCDFT A
	                 WHERE A.FACTORY = P_FACTORY
	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                      END  
	                   AND A.WORK_DATE <= P_BASE_TIME
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%'
	                 GROUP BY A.DEFECT_CODE) A
	             , (SELECT SUM(A.QC_LOT_QTY_1) AS SAMPLE_QTY_1
	                  FROM RSUMPQCLOT A
	                 WHERE A.FACTORY = P_FACTORY
	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 0, 'YYYYMMDD')
	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 1, 'YYYYMMDD')
	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 2, 'YYYYMMDD')
	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 3, 'YYYYMMDD')
	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 4, 'YYYYMMDD')
	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 5, 'YYYYMMDD')
	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE(P_BASE_TIME, 'YYYYMMDD') - 6, 'YYYYMMDD')
	                                      END  
	                   AND A.WORK_DATE <= P_BASE_TIME
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%') B
	         WHERE A.ROW_LEVEL <= V_DISPLAY_COUNT) B
		 , (SELECT A.ROW_LEVEL AS ROW_LEVEL 
	             , A.DEFECT_CODE AS DEFECT_CODE_DD
	             , A.DEFECT_QTY_1 AS DEFECT_QTY_DD
	             , B.SAMPLE_QTY_1 AS SAMPLE_QTY_DD
	          FROM (SELECT A.DEFECT_CODE AS DEFECT_CODE
	                     , SUM(A.DEFECT_LOT_QTY_1) AS DEFECT_QTY_1
	                     , ROW_NUMBER() OVER(ORDER BY SUM(A.DEFECT_LOT_QTY_1) DESC) AS ROW_LEVEL
	                  FROM RSUMPQCDFT A
	                 WHERE A.FACTORY = P_FACTORY
	                   AND A.WORK_DATE >= P_BASE_TIME
	                   AND A.WORK_DATE <= P_BASE_TIME
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%'
	                 GROUP BY A.DEFECT_CODE) A
	             , (SELECT SUM(A.QC_LOT_QTY_1) AS SAMPLE_QTY_1
	                  FROM RSUMPQCLOT A
	                 WHERE A.FACTORY = P_FACTORY
	                   AND A.WORK_DATE >= P_BASE_TIME
	                   AND A.WORK_DATE <= P_BASE_TIME
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%') B
	         WHERE A.ROW_LEVEL <= 5) C
	 WHERE A.ROW_LEVEL = B.ROW_LEVEL(+)
	   AND A.ROW_LEVEL = C.ROW_LEVEL(+)
	 ORDER BY A.ROW_LEVEL ASC;

	PACKAGE_RESULT := PROCEDURE_RESULT;
	
EXCEPTION
    WHEN OTHERS THEN
	ROLLBACK;
END;


/************************************************************************************************************************************************************************************/
/************************************************************************************************************************************************************************************/


PROCEDURE PROC_PQC030107_DAYS
(
		P_FACTORY		IN		VARCHAR2,			-- 공장구분
		P_BASE_MONTH	IN		VARCHAR2, 			-- 기준년월
		P_BASE_TIME		IN		VARCHAR2, 			-- 기준일자
		P_QC_OPER		IN		VARCHAR2,			-- 검사공정
		P_QC_TYPE		IN		VARCHAR2,			-- 검사목적
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
)
/************************************************************
 * COMMENT : 공정검사 품질불량현황 일자별 데이터를 조회한다.
 *								
 * CREATED BY : BEE-JAE JUNG(2010-06-29-화요일)
 *											
 * MODIFIED BY : BEE-JAE JUNG(2010-06-29-화요일)
 ************************************************************/

/************************************************************************************************************
 COMMENT : REF CURDOR로 RETURN되는 SQL문을 TOAD, ORANGE에서 실행결과 확인법
 
 BEGIN
     PACKAGE_PQC030107.PROC_PQC030107_DAYS('HMKA1', '201008', '20100831', '%', '%', :CURSOR_OUT);
 END;
 ************************************************************************************************************/
IS
	-- 지역 변수 선언	
	PROCEDURE_RESULT	PKGCUR;
	V_DISPLAY_COUNT		INT;
BEGIN
	
	-- 2010-06-29-정비재 : 항목별 표시할 데이터 건수를 지정한다.
	V_DISPLAY_COUNT := 5;

	OPEN PROCEDURE_RESULT FOR
	SELECT B.DEFECT_CODE_PREV AS DEFECT_CODE_PREV 
		 , B.DEFECT_QTY_PREV AS DEFECT_QTY_PREV 
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '01' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_01
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '02' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_02
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '03' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_03
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '04' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_04
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '05' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_05
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '06' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_06
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '07' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_07
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '08' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_08
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '09' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_09
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '10' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_10
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '11' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_11
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '12' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_12
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '13' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_13
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '14' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_14
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '15' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_15
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '16' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_16
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '17' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_17
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '18' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_18
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '19' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_19
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '20' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_20
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '21' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_21
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '22' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_22
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '23' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_23
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '24' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_24
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '25' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_25
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '26' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_26
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '27' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_27
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '28' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_28
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '29' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_29
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '30' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_30
		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '31' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_31
	  FROM RSUMPQCDFT A
		 , (SELECT A.DEFECT_CODE AS DEFECT_CODE_PREV
	             , A.DEFECT_QTY_1 AS DEFECT_QTY_PREV
	             , A.ROW_LEVEL AS ROW_LEVEL_PREV
	          FROM (SELECT A.DEFECT_CODE AS DEFECT_CODE 
	                     , SUM(A.DEFECT_QTY_1) AS DEFECT_QTY_1
	                     , ROW_NUMBER() OVER(ORDER BY SUM(A.DEFECT_QTY_1) DESC) AS ROW_LEVEL 
	                  FROM RSUMPQCDFT A 
	                 WHERE A.FACTORY = P_FACTORY
	                   AND A.WORK_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE(P_BASE_MONTH, 'YYYYMM'), -1), 'YYYYMM')
	                   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	                   AND A.QC_TYPE LIKE P_QC_TYPE || '%' 
	                 GROUP BY A.DEFECT_CODE) A
	         WHERE A.ROW_LEVEL <= V_DISPLAY_COUNT) B
	 WHERE A.DEFECT_CODE = B.DEFECT_CODE_PREV
	   AND A.FACTORY = P_FACTORY
	   AND A.WORK_MONTH = P_BASE_MONTH
	   AND A.QC_OPER LIKE P_QC_OPER || '%' 
	   AND A.QC_TYPE LIKE P_QC_TYPE || '%'
	 GROUP BY B.DEFECT_CODE_PREV, B.DEFECT_QTY_PREV, B.ROW_LEVEL_PREV
	 ORDER BY B.ROW_LEVEL_PREV ASC;

	PACKAGE_RESULT := PROCEDURE_RESULT;
	
EXCEPTION
    WHEN OTHERS THEN
	ROLLBACK;
END;


/************************************************************************************************************************************************************************************/
/************************************************************************************************************************************************************************************/


END PACKAGE_PQC030107;

