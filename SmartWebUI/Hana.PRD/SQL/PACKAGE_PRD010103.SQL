CREATE PACKAGE RPTMGR.PACKAGE_PRD010103
IS
	/****************************************************
	 * COMMENT : GCT고객사의 WIP Report를 조회한다.
	 *								
	 * CREATED BY : BEE-JAE JUNG(2010-12-15-수요일)
	 *											
	 * MODIFIED BY : BEE-JAE JUNG(2011-07-12-화요일)
	 ****************************************************/

	TYPE PKGCUR IS REF CURSOR;

	-- 01. GCT고객사의 WIP REPORT를 조회한다.
	PROCEDURE PROC_PRD010103
	(
		P_MAT_ID		IN		VARCHAR2,			-- 제품코드
		P_LOT_TYPE		IN		VARCHAR2,			-- LOT TYPE
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
	);
	
	-- 02. GCT WIP REPORT를 TABLE에 저장한다.
	PROCEDURE PROC_PRD010103_T
	(
		P_MAT_ID		IN		VARCHAR2,			-- 제품코드
		P_LOT_TYPE		IN		VARCHAR2			-- LOT TYPE
	);
	
END PACKAGE_PRD010103;


/************************************************************************************************************************************************************************************/
/************************************************************************************************************************************************************************************/


CREATE PACKAGE BODY RPTMGR.PACKAGE_PRD010103 
AS

PROCEDURE PROC_PRD010103
(
		P_MAT_ID		IN		VARCHAR2,			-- 제품코드
		P_LOT_TYPE		IN		VARCHAR2,			-- LOT TYPE
		PACKAGE_RESULT	OUT		PKGCUR				-- 검색된 결과를 반환한다.
)
/************************************************************************************************************
 COMMENT : REF CURDOR로 RETURN되는 SQL문을 TOAD에서 실행결과 확인법
 
 BEGIN
     PACKAGE_PRD010103.PROC_PRD010103('%', '%', :CURSOR_OUT);
 END;
 ************************************************************************************************************/
IS
	-- 2012-06-22-정비재 : LOG기록을 위한 변수를 선언한다.
	P_PROC_NAME			VARCHAR2(30);
	P_PROC_DESC			VARCHAR2(50);
	P_START_TIME		VARCHAR2(14);
	P_END_TIME			VARCHAR2(14);
	P_EXE_COUNT			NUMBER;
    P_ERR_COUNT			NUMBER;
    
	-- 지역 변수 선언	
	PROCEDURE_RESULT	PKGCUR;
BEGIN
	
	-- 2012-06-22-정비재 : LOG기록을 위하여 변수에 데이터를 입력한다.
	P_PROC_NAME := 'PROC_PRD010103';
    P_PROC_DESC	:= 'MAT_ID:' || P_MAT_ID || ',LOT_TYPE:' || P_LOT_TYPE;
    P_START_TIME := TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS');
    P_END_TIME := ' ';
    P_EXE_COUNT	:= 0;
    P_ERR_COUNT := 0;
    
	-- LOG 기록
	-- PROC를 시작할 때 LOG기록을 한 번 한다.(PROC의 RUNNING TIME을 CHECK하기 위하여)
	MESMGR.SET_INFO_STS_LOG@RPTTOMES(P_PROC_NAME, P_PROC_DESC, P_START_TIME, P_START_TIME, P_EXE_COUNT, P_ERR_COUNT, 'S');
	
	-- 2010-12-15-정비재 : GCT고객사의 WIP REPORT를 조회한다.
 	OPEN PROCEDURE_RESULT FOR
	SELECT NVL(K.GCT_PROJECT_NO, ' ') AS GCT_PROJECT_NO 
	     , NVL(A.GCT_DEVICE, ' ') AS GCT_DEVICE 
	     , NVL(A.PACKAGE, ' ') AS PACKAGE 
	     , NVL(A.LD_COUNT, ' ') AS LD_COUNT 
	     , NVL(A.DIMENSION, ' ') AS DIMENSION 
	     , NVL(A.VENDOR_PART_NO, ' ') AS VENDOR_PART_NO 
	     , NVL(K.WFR_LOT_ID, ' ' ) AS WFR_LOT_ID 
	     , NVL(A.GCT_ASSY_LOT_ID, ' ') AS GCT_ASSY_LOT_ID 
	     , NVL(A.GCT_TEST_LOT_ID, ' ') AS GCT_TEST_LOT_ID 
	     , NVL(A.VENDOR_LOT_ID, ' ') AS VENDOR_LOT_ID 
	     , ' ' AS PROD_RLSESCHED_NO 
	     , ' ' AS VENDOR_ORDER_NO 
	     , NVL(A.TEST_RCVD_QTY, 0) AS TEST_RCVD_QTY 
	     , CASE WHEN NVL(A.TEST_RCVD_DATE, ' ') <> ' ' THEN TO_CHAR(TO_DATE(NVL(A.TEST_RCVD_DATE, ' '), 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') 
	                                                   ELSE ' ' 
	       END AS TEST_RCVD_DATE 
	     , NVL(B.TEST_QUE, 0) AS TEST_QUE 
	     , CASE WHEN NVL(A.TEST_START_DATE, ' ') <> ' ' THEN TO_CHAR(TO_DATE(NVL(A.TEST_START_DATE, ' '), 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') 
	                                                    ELSE ' ' 
	       END AS TEST_START_DATE 
	     , NVL(C.FINAL_TEST_CAS_OS, 0) AS FINAL_TEST_CAS_OS 
	     , NVL(D.QA_EL_TEST, 0) AS QA_EL_TEST 
	     , NVL(E.TEST_HOLD, 0) AS TEST_HOLD 
	     , CASE WHEN NVL(F.LOT_HOLD_DATE, ' ') <> ' ' THEN TO_CHAR(TO_DATE(NVL(F.LOT_HOLD_DATE, ' '), 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') 
	                                                  ELSE ' ' 
	       END AS LOT_HOLD_DATE 
	     , CASE WHEN NVL(G.LOT_HOLD_REL_DATE, ' ') <> ' ' THEN TO_CHAR(TO_DATE(NVL(G.LOT_HOLD_REL_DATE, ' '), 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') 
	                                                      ELSE ' ' 
	       END AS LOT_HOLD_REL_DATE 
	     , NVL(H.TEST_OUT_QTY, 0) AS TEST_OUT_QTY 
	     , ' ' AS TEST_OUT_FCST_DATE 
	     , CASE WHEN NVL(H.TEST_OUT_DATE, ' ') <> ' ' THEN TO_CHAR(TO_DATE(NVL(H.TEST_OUT_DATE, ' '), 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') 
	                                                  ELSE ' ' 
	       END AS TEST_OUT_DATE 
	     , NVL(I.VM, 0) AS VM 
	     , NVL(I.BAKE, 0) AS BAKE 
	     , NVL(I.TNR, 0) AS TNR 
	     --, NVL(I.PACK, 0) AS PACK
	     , CASE WHEN NVL(I.PACK, 0) <= 0 THEN NVL(I.PACK, 0)
	     		ELSE NVL(I.PACK, 0) + NVL(H.PACK_OUT_QTY, 0)
	       END AS PACK
	     --, NVL(H.PACK_OUT_QTY, 0) AS PACK_OUT_QTY
		 , CASE WHEN NVL(I.PACK, 0) <= 0 THEN NVL(H.PACK_OUT_QTY, 0)
	     		ELSE 0
		   END AS PACK_OUT_QTY
	     , CASE WHEN NVL(H.PACK_OUT_DATE, ' ') <> ' ' THEN TO_CHAR(TO_DATE(NVL(H.PACK_OUT_DATE, ' '), 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') 
	                                                  ELSE ' ' 
	       END AS PACK_OUT_DATE 
	     --, NVL(H.PACK_OUT_QTY, 0) - NVL(J.SHIP_OUT_QTY, 0) AS FGS_QTY
	     , CASE WHEN NVL(I.PACK, 0) <= 0 THEN NVL(H.PACK_OUT_QTY, 0) - NVL(J.SHIP_OUT_QTY, 0)
	     		ELSE 0
	       END AS FGS_QTY
	     , NVL(J.SHIP_OUT_QTY, 0) AS SHIP_OUT_QTY 
	     , ' ' AS FCST_SHIP_DATE 
	     , CASE WHEN NVL(J.ACT_SHIP_TIME, ' ') <> ' ' THEN TO_CHAR(TO_DATE(NVL(J.ACT_SHIP_TIME, ' '), 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') 
	                                                  ELSE ' ' 
	       END AS ACT_SHIP_TIME 
	     , ' ' AS CUST_REQ_DATE 
	     , NVL(K.PR_NO, ' ') AS PR_NO 
	     , NVL(A.TEST_PO, ' ') AS TEST_PO 
	     , NVL(F.HOLD_CODE, ' ') AS HOLD_CODE 
	     --, NVL(A.TEST_RCVD_QTY, 0) - (NVL(B.TEST_QUE, 0) + NVL(D.QA_EL_TEST, 0) + NVL(C.FINAL_TEST_CAS_OS, 0) + NVL(E.TEST_HOLD, 0) + NVL(I.VM, 0) + NVL(I.BAKE, 0) + NVL(I.TNR, 0) + NVL(I.PACK, 0) + NVL(H.PACK_OUT_QTY, 0)) AS REJECT
	     , NVL(A.TEST_RCVD_QTY, 0) 
	    - (NVL(B.TEST_QUE, 0) + NVL(D.QA_EL_TEST, 0) + NVL(C.FINAL_TEST_CAS_OS, 0) + NVL(I.VM, 0) + NVL(I.BAKE, 0) + NVL(I.TNR, 0) 
	     + (CASE WHEN NVL(I.PACK, 0) <= 0 THEN NVL(I.PACK, 0) ELSE NVL(I.PACK, 0) + NVL(H.PACK_OUT_QTY, 0) END) 
	     + (CASE WHEN NVL(I.PACK, 0) <= 0 THEN NVL(H.PACK_OUT_QTY, 0) ELSE 0 END)) AS REJECT
	     , A.REMARK AS REMARK
	     , SUBSTR(TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS'), 1, 10) || '0000' AS BATCH_TIME
	  FROM (SELECT A.GCT_ASSY_LOT_ID AS GCT_ASSY_LOT_ID 
	             , A.GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID 
	             , A.VENDOR_LOT_ID AS VENDOR_LOT_ID 
	             , A.VENDOR_PART_NO AS VENDOR_PART_NO 
	             , A.TEST_RCVD_QTY AS TEST_RCVD_QTY 
	             , A.TEST_RCVD_DATE AS TEST_RCVD_DATE 
	             , A.TEST_START_DATE AS TEST_START_DATE 
	             , B.GCT_DEVICE AS GCT_DEVICE 
	             , B.PACKAGE AS PACKAGE 
	             , B.LD_COUNT AS LD_COUNT 
	             , B.DIMENSION AS DIMENSION 
	             , A.TEST_PO AS TEST_PO 
	             , A.REMARK AS REMARK 
	          FROM (SELECT B.GCT_ASSY_LOT_ID AS GCT_ASSY_LOT_ID 
	                     , B.GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID 
	                     , B.VENDOR_LOT_ID AS VENDOR_LOT_ID 
	                     , B.VENDOR_PART_NO AS VENDOR_PART_NO 
	                     , A.CREATE_QTY_1 AS TEST_RCVD_QTY 
	                     , B.TEST_RCVD_DATE AS TEST_RCVD_DATE 
	                     , B.TEST_START_DATE AS TEST_START_DATE 
	                     , B.TEST_PO AS TEST_PO 
	                     , B.REMARK AS REMARK 
	                  FROM RWIPLOTSTS A, (SELECT GCT_ASSY_LOT_ID AS GCT_ASSY_LOT_ID
	                                           , GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                                           , VENDOR_LOT_ID AS VENDOR_LOT_ID
	                                           , MAX(VENDOR_PART_NO) AS VENDOR_PART_NO
	                                           , MIN(TEST_RCVD_DATE) AS TEST_RCVD_DATE 
	                                           , MIN(TEST_START_DATE) AS TEST_START_DATE 
	                                           , MAX(TEST_PO) AS TEST_PO 
	                                           , MAX(REMARK) AS REMARK
	                                        FROM (SELECT SUBSTR(LOT_CMF_4, 1, 9) || '0' AS GCT_ASSY_LOT_ID 
	                                                   , LOT_CMF_4 AS GCT_TEST_LOT_ID 
	                                                   , LOT_CMF_4 AS VENDOR_LOT_ID 
	                                                   , MAT_ID AS VENDOR_PART_NO 
	                                                   , MIN(CREATE_TIME) AS TEST_RCVD_DATE 
	                                                   , MIN(OPER_IN_TIME) AS TEST_START_DATE 
	                                                   , MAX(LOT_CMF_3) AS TEST_PO 
	                                                   , MAX(LAST_COMMENT) AS REMARK 
	                                                FROM RWIPLOTSTS 
	                                               WHERE FACTORY = 'HMKT1' 
	                                                 AND MAT_ID LIKE 'GC%'
	                                                 AND LOT_TYPE = 'W' 
	                                                 AND LOT_DEL_FLAG = ' ' 
	                                                 AND QTY_1 > 0 
	                                                 AND MAT_ID LIKE P_MAT_ID || '%' 
	                                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	                                               GROUP BY SUBSTR(LOT_CMF_4, 1, 9) || '0', LOT_CMF_4, MAT_ID
	                                              UNION ALL
	                                              SELECT SUBSTR(A.LOT_CMF_4, 1, 9) || '0' AS GCT_ASSY_LOT_ID 
	                                                   , A.LOT_CMF_4 AS GCT_TEST_LOT_ID 
	                                                   , A.LOT_CMF_4 AS VENDOR_LOT_ID 
	                                                   , A.MAT_ID AS VENDOR_PART_NO 
	                                                   , MIN(A.CREATE_TIME) AS TEST_RCVD_DATE 
	                                                   , MIN(A.OPER_IN_TIME) AS TEST_START_DATE 
	                                                   , MAX(A.LOT_CMF_3) AS TEST_PO 
	                                                   , MAX(A.LAST_COMMENT) AS REMARK
	                                                FROM RWIPLOTHIS A, (SELECT A.FROM_TO_LOT_ID AS MERGE_LOT
	                                                                         , A.OPER AS MERGE_OPER
	                                                                         , B.LOT_ID AS LOT_ID
	                                                                         , B.LOT_CMF_4 AS LOT_CMF_4
	                                                                      FROM RWIPLOTMRG A, (SELECT LOT_ID AS LOT_ID
	                                                                                               , LOT_CMF_4  AS LOT_CMF_4
	                                                                                            FROM RWIPLOTSTS
	                                                                                           WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                                             AND MAT_ID LIKE 'GC%' 
	                                                                                             AND LOT_TYPE = 'W' 
	                                                                                             AND LOT_DEL_FLAG = ' '
	                                                                                             AND QTY_1 > 0 
	                                                                                             AND MAT_ID LIKE P_MAT_ID || '%' 
	                                                                                             AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' ) B
	                                                                     WHERE A.LOT_ID = B.LOT_ID
	                                                                       AND A.FACTORY = 'HMKT1' 
	                                                                       AND A.OPER = 'T1300'
	                                                                       AND A.MAT_ID LIKE 'GC%' 
	                                                                       AND A.HIST_DEL_FLAG = ' ') B
	                                               WHERE A.LOT_ID = B.MERGE_LOT
	                                                 AND A.OPER = B.MERGE_OPER
	                                                 AND A.TRAN_CODE = 'MERGE'
	                                               GROUP BY SUBSTR(A.LOT_CMF_4, 1, 9) || '0', A.LOT_CMF_4, A.LOT_CMF_4, A.MAT_ID)
	                                        GROUP BY GCT_ASSY_LOT_ID, GCT_TEST_LOT_ID, VENDOR_LOT_ID) B 
	                 WHERE B.GCT_TEST_LOT_ID = A.LOT_ID(+)) A 
	             , (SELECT MAT_ID AS VENDOR_PART_NO 
	                     , MAT_CMF_7 AS GCT_DEVICE 
	                     , MAT_GRP_3 AS PACKAGE 
	                     , MAT_GRP_6 AS LD_COUNT 
	                     , MAT_CMF_9 AS DIMENSION 
	                  FROM MWIPMATDEF 
	                 WHERE FACTORY = 'HMKT1' 
	                   AND MAT_GRP_1 = 'GC' 
	                   AND DELETE_FLAG = ' ') B 
	        WHERE A.VENDOR_PART_NO = B.VENDOR_PART_NO(+)) A 
	     , (SELECT LOT_CMF_4 AS GCT_TEST_LOT_ID 
	             , SUM(QTY_1) AS TEST_QUE 
	          FROM RWIPLOTSTS 
	         WHERE FACTORY = 'HMKT1' 
	           AND MAT_ID LIKE 'GC%' 
	           AND OPER = 'T0000' 
	           AND LOT_TYPE = 'W' 
	           AND LOT_DEL_FLAG = ' ' 
	           AND QTY_1 > 0 
	           AND MAT_ID LIKE P_MAT_ID || '%' 
	           AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	         GROUP BY LOT_CMF_4) B 
	     , (SELECT LOT_CMF_4 AS GCT_TEST_LOT_ID 
	             , SUM(QTY_1) AS FINAL_TEST_CAS_OS 
	          FROM RWIPLOTSTS 
	         WHERE FACTORY = 'HMKT1' 
	           AND MAT_ID LIKE 'GC%' 
	           AND OPER IN ('T0090', 'T0100', 'T0650', 'T0900') 
	           AND LOT_TYPE = 'W' 
	           AND LOT_DEL_FLAG = ' ' 
	           AND HOLD_FLAG = ' ' 
	           AND QTY_1 > 0 
	           AND MAT_ID LIKE P_MAT_ID || '%' 
	           AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	         GROUP BY LOT_CMF_4) C 
	     , (SELECT LOT_CMF_4 AS GCT_TEST_LOT_ID 
	             , SUM(QTY_1) AS QA_EL_TEST 
	          FROM RWIPLOTSTS 
	         WHERE FACTORY = 'HMKT1' 
	           AND MAT_ID LIKE 'GC%' 
	           AND OPER IN ('T0300', 'T1040') 
	           AND LOT_TYPE = 'W' 
	           AND LOT_DEL_FLAG = ' ' 
	           AND QTY_1 > 0 
	           AND MAT_ID LIKE P_MAT_ID || '%' 
	           AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	         GROUP BY LOT_CMF_4) D 
	     , (SELECT LOT_CMF_4 AS GCT_TEST_LOT_ID 
	             , SUM(QTY_1) AS TEST_HOLD  
	          FROM RWIPLOTSTS 
	         WHERE FACTORY = 'HMKT1' 
	           AND MAT_ID LIKE 'GC%' 
	           AND OPER IN ('T0090', 'T0100', 'T0650', 'T0900') 
	           AND LOT_TYPE = 'W' 
	           AND HOLD_FLAG = 'Y' 
	           AND LOT_DEL_FLAG = ' ' 
	           AND QTY_1 > 0 
	           AND MAT_ID LIKE P_MAT_ID || '%' 
	           AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	         GROUP BY LOT_CMF_4) E 
	     , (SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	             , LOT_HOLD_DATE AS LOT_HOLD_DATE
	             , HOLD_CODE AS HOLD_CODE
	          FROM (SELECT LOT_CMF_4 AS GCT_TEST_LOT_ID 
	                     , MAX(LAST_TRAN_TIME) AS LOT_HOLD_DATE 
	                     , HOLD_CODE AS HOLD_CODE
	                  FROM RWIPLOTSTS 
	                 WHERE FACTORY = 'HMKT1' 
	                   AND MAT_ID LIKE 'GC%' 
	                   AND LOT_TYPE = 'W' 
	                   AND HOLD_FLAG = 'Y' 
	                   AND LOT_DEL_FLAG = ' ' 
	                   AND QTY_1 > 0 
	                   AND MAT_ID LIKE P_MAT_ID || '%' 
	                   AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	                 GROUP BY LOT_CMF_4, HOLD_CODE
	                 ORDER BY LOT_HOLD_DATE DESC)
	         WHERE ROWNUM = 1) F 
	     , (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID 
	             , MAX(A.RELEASE_TRAN_TIME) AS LOT_HOLD_REL_DATE 
	          FROM RWIPLOTHLD A, (SELECT LOT_ID AS LOT_ID
	                                   , LOT_CMF_4 AS LOT_CMF_4
	                                FROM RWIPLOTSTS 
	                               WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                 AND MAT_ID LIKE 'GC%' 
	                                 AND LOT_TYPE = 'W' 
	                                 AND LOT_DEL_FLAG = ' ' 
	                                 AND QTY_1 > 0 
	                                 AND MAT_ID LIKE P_MAT_ID || '%' 
	                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' ) B 
	         WHERE A.LOT_ID = B.LOT_ID 
	           AND A.FACTORY = 'HMKT1' 
	           AND A.MAT_ID LIKE 'GC%' 
	           AND A.HIST_DEL_FLAG = ' ' 
	         GROUP BY LOT_CMF_4) G 
	     , (SELECT GCT_TEST_LOT_ID
	             , MAX(TEST_OUT_DATE) AS TEST_OUT_DATE
	             , SUM(TEST_OUT_QTY) AS TEST_OUT_QTY
	             , MAX(PACK_OUT_DATE) AS PACK_OUT_DATE
	             , SUM(PACK_OUT_QTY) AS PACK_OUT_QTY
	          FROM (SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                     , TEST_OUT_DATE AS TEST_OUT_DATE
	                     , SUM(TEST_OUT_QTY) AS TEST_OUT_QTY
	                     , MAX(PACK_OUT_DATE) AS PACK_OUT_DATE 
	                     , SUM(PACK_OUT_QTY) AS PACK_OUT_QTY
	                  FROM (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                             , MAX(A.TRAN_TIME) AS TEST_OUT_DATE
	                             , SUM(A.QTY_1) AS TEST_OUT_QTY
	                             , ' ' AS PACK_OUT_DATE
	                             , 0 AS PACK_OUT_QTY
	                          FROM RWIPLOTHIS A, (SELECT LOT_ID AS LOT_ID
	                                                   , LOT_CMF_4 AS LOT_CMF_4
	                                                FROM RWIPLOTSTS
	                                               WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                 AND MAT_ID LIKE 'GC%' 
	                                                 AND LOT_TYPE = 'W' 
	                                                 AND MAT_ID LIKE P_MAT_ID || '%'
	                                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                         WHERE A.LOT_ID = B.LOT_ID
	                           AND A.FACTORY = 'HMKT1'
	                           AND A.OLD_OPER = 'T1040'
	                           AND A.TRAN_CODE = 'END'
	                           AND A.RWK_FLAG = 'Y'
	                           AND A.HIST_DEL_FLAG = ' '
	                         GROUP BY B.LOT_CMF_4
	                        UNION ALL
	                        SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                             , MAX(A.TRAN_TIME) AS TEST_OUT_DATE
	                             , SUM(A.QTY_1) AS TEST_OUT_QTY
	                             , ' ' AS PACK_OUT_DATE
	                             , 0 AS PACK_OUT_QTY
	                          FROM RWIPLOTHIS A, (SELECT LOT_ID AS LOT_ID
	                                                   , LOT_CMF_4 AS LOT_CMF_4
	                                                FROM RWIPLOTSTS
	                                               WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                 AND MAT_ID LIKE 'GC%' 
	                                                 AND LOT_TYPE = 'W' 
	                                                 AND MAT_ID LIKE P_MAT_ID || '%'
	                                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                         WHERE A.LOT_ID = B.LOT_ID
	                           AND A.FACTORY = 'HMKT1'
	                           AND A.OLD_OPER = 'T1040'
	                           AND A.TRAN_CODE = 'END'
	                           AND A.RWK_FLAG = ' '
	                           AND A.HIST_DEL_FLAG = ' '
	                           AND A.LOT_ID NOT IN (SELECT A.LOT_ID
	                                                  FROM RWIPLOTHIS A, (SELECT LOT_ID AS LOT_ID
	                                                                           , LOT_CMF_4 AS LOT_CMF_4
	                                                                        FROM RWIPLOTSTS
	                                                                       WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                         AND MAT_ID LIKE 'GC%' 
	                                                                         AND LOT_TYPE = 'W' 
	                                                                         AND MAT_ID LIKE P_MAT_ID || '%'
	                                                                         AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                                                 WHERE A.LOT_ID = B.LOT_ID
	                                                   AND A.FACTORY = 'HMKT1'
	                                                   AND A.OLD_OPER = 'T1040'
	                                                   AND A.TRAN_CODE = 'END'
	                                                   AND A.RWK_FLAG = 'Y'
	                                                   AND A.HIST_DEL_FLAG = ' ')
	                         GROUP BY B.LOT_CMF_4)
	                 GROUP BY GCT_TEST_LOT_ID, TEST_OUT_DATE
	                UNION ALL
	                -- PACK_OUT_QTY를 계산하는 QUERY
	                SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                     , MAX(TEST_OUT_DATE) AS TEST_OUT_DATE
	                     , SUM(TEST_OUT_QTY) AS TEST_OUT_QTY
	                     , MAX(PACK_OUT_DATE) AS PACK_OUT_DATE
	                     , SUM(PACK_OUT_QTY) AS PACK_OUT_QTY 
	                  FROM (-- (+) REWORK으로 PACK OUT 된 수량
	                        SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                             , ' ' AS TEST_OUT_DATE
	                             , 0 AS TEST_OUT_QTY
	                             , MAX(A.TRAN_TIME) AS PACK_OUT_DATE
	                             , SUM(A.QTY_1) AS PACK_OUT_QTY
	                          FROM RWIPLOTHIS A, (SELECT LOT_ID AS LOT_ID
	                                                   , LOT_CMF_4 AS LOT_CMF_4
	                                                FROM RWIPLOTSTS
	                                               WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                 AND MAT_ID LIKE 'GC%' 
	                                                 AND LOT_TYPE = 'W' 
	                                                 AND MAT_ID LIKE P_MAT_ID || '%'
	                                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                         WHERE A.LOT_ID = B.LOT_ID
	                           AND A.FACTORY = 'HMKT1'
	                           AND A.OLD_OPER = 'T1300'
	                           AND A.TRAN_CODE = 'END'
	                           AND A.RWK_FLAG = 'Y'
	                           AND A.HIST_DEL_FLAG = ' '
	                         GROUP BY B.LOT_CMF_4
	                        UNION ALL
	                        -- (+) 정상적인 작업은 PACK OUT 된 수량
	                        SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                             , ' ' AS TEST_OUT_DATE
	                             , 0 AS TEST_OUT_QTY
	                             , MAX(A.TRAN_TIME) AS PACK_OUT_DATE
	                             , SUM(A.QTY_1) AS PACK_OUT_QTY
	                          FROM RWIPLOTHIS A, (SELECT LOT_ID AS LOT_ID
	                                                   , LOT_CMF_4 AS LOT_CMF_4
	                                                FROM RWIPLOTSTS
	                                               WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                 AND MAT_ID LIKE 'GC%' 
	                                                 AND LOT_TYPE = 'W' 
	                                                 AND MAT_ID LIKE P_MAT_ID || '%'
	                                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                         WHERE A.LOT_ID = B.LOT_ID
	                           AND A.FACTORY = 'HMKT1'
	                           AND A.OLD_OPER = 'T1300'
	                           AND A.TRAN_CODE = 'END'
	                           AND A.RWK_FLAG = ' '
	                           AND A.HIST_DEL_FLAG = ' '
	                           AND A.LOT_ID NOT IN (SELECT A.LOT_ID
	                                                  FROM RWIPLOTHIS A, (SELECT LOT_ID AS LOT_ID
	                                                                           , LOT_CMF_4 AS LOT_CMF_4
	                                                                        FROM RWIPLOTSTS
	                                                                       WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                         AND MAT_ID LIKE 'GC%' 
	                                                                         AND LOT_TYPE = 'W' 
	                                                                         AND MAT_ID LIKE P_MAT_ID || '%'
	                                                                         AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                                                 WHERE A.LOT_ID = B.LOT_ID
	                                                   AND A.FACTORY = 'HMKT1'
	                                                   AND A.OLD_OPER = 'T1300'
	                                                   AND A.TRAN_CODE = 'END'
	                                                   AND A.RWK_FLAG = 'Y'
	                                                   AND A.HIST_DEL_FLAG = ' ')
	                         GROUP BY B.LOT_CMF_4
	                        UNION ALL
	                        -- (-)본 LOT에 다른 LOT이 MERGE되어 PACK OUT 된 수량
	                        SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                             , MAX(TEST_OUT_DATE) AS TEST_OUT_DATE
	                             , SUM(TEST_OUT_QTY) AS TEST_OUT_QTY
	                             , MAX(PACK_OUT_DATE) AS PACK_OUT_DATE
	                             , -SUM(PACK_OUT_QTY) AS PACK_OUT_QTY
	                          FROM (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                                     , ' ' AS TEST_OUT_DATE
	                                     , 0 AS TEST_OUT_QTY
	                                     , ' ' AS PACK_OUT_DATE
	                                     , CASE WHEN A.OLD_QTY_1 > A.QTY_1 THEN A.OLD_QTY_1 - A.QTY_1
	                                                                       ELSE 0
	                                       END AS PACK_OUT_QTY
	                                  FROM RWIPLOTHIS A, (SELECT A.FROM_TO_LOT_ID AS MERGE_LOT
	                                                           , A.OPER AS MERGE_OPER
	                                                           , B.LOT_ID AS LOT_ID
	                                                           , B.LOT_CMF_4 AS LOT_CMF_4
	                                                        FROM RWIPLOTMRG A, (SELECT LOT_ID AS LOT_ID
	                                                                                 , LOT_CMF_4 AS LOT_CMF_4
	                                                                              FROM RWIPLOTSTS
	                                                                             WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                               AND MAT_ID LIKE 'GC%' 
	                                                                               AND LOT_TYPE = 'W' 
	                                                                               AND MAT_ID LIKE P_MAT_ID || '%'
	                                                                               AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                                                       WHERE A.LOT_ID = B.LOT_ID
	                                                         AND A.FACTORY = 'HMKT1' 
	                                                         AND A.OPER = 'T1300'
	                                                         AND A.MAT_ID LIKE 'GC%' 
	                                                         AND A.FROM_TO_FLAG = 'T'
	                                                         AND A.HIST_DEL_FLAG = ' ') B
	                                 WHERE A.LOT_ID = B.MERGE_LOT
	                                   AND A.OPER = B.MERGE_OPER
	                                   AND A.FROM_TO_LOT_ID = B.LOT_ID
	                                   AND A.LOT_CMF_4 <> B.LOT_CMF_4
	                                   AND A.TRAN_CODE = 'MERGE'
	                                   AND A.HIST_DEL_FLAG = ' ')
	                         GROUP BY GCT_TEST_LOT_ID
	                        UNION ALL
	                        -- (+)본 LOT이 다른LOT에 MERGE되어 PACK OUT된 수량
	                        SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                             , MAX(TEST_OUT_DATE) AS TEST_OUT_DATE
	                             , SUM(TEST_OUT_QTY) AS TEST_OUT_QTY
	                             , MAX(PACK_OUT_DATE) AS PACK_OUT_DATE
	                             , SUM(PACK_OUT_QTY) AS PACK_OUT_QTY
	                          FROM (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                                     , ' ' AS TEST_OUT_DATE
	                                     , 0 AS TEST_OUT_QTY
	                                     , ' ' AS PACK_OUT_DATE
	                                     , CASE WHEN A.OLD_QTY_1 > A.QTY_1 THEN A.OLD_QTY_1 - A.QTY_1
	                                                                       ELSE 0
	                                       END AS PACK_OUT_QTY
	                                  FROM RWIPLOTHIS A, (SELECT A.LOT_ID AS MERGE_LOT
	                                                           , A.OPER AS MERGE_OPER
	                                                           , A.FROM_TO_LOT_ID AS FROM_TO_LOT_ID
	                                                           , B.LOT_ID AS LOT_ID
	                                                           , B.LOT_CMF_4 AS LOT_CMF_4
	                                                        FROM RWIPLOTMRG A, (SELECT LOT_ID AS LOT_ID
	                                                                                 , LOT_CMF_4 AS LOT_CMF_4
	                                                                              FROM RWIPLOTSTS
	                                                                             WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                               AND MAT_ID LIKE 'GC%' 
	                                                                               AND LOT_TYPE = 'W' 
	                                                                               AND MAT_ID LIKE P_MAT_ID || '%'
	                                                                               AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                                                       WHERE A.LOT_ID = B.LOT_ID
	                                                         AND A.FACTORY = 'HMKT1' 
	                                                         AND A.OPER = 'T1300'
	                                                         AND A.MAT_ID LIKE 'GC%'
	                                                         AND A.FROM_TO_FLAG = 'F'
	                                                         AND A.HIST_DEL_FLAG = ' '
	                                                         AND SUBSTR(A.FROM_TO_LOT_ID, 1, LENGTH(B.LOT_CMF_4)) <> B.LOT_CMF_4) B
	                                 WHERE A.LOT_ID = B.MERGE_LOT
	                                   AND A.OPER = B.MERGE_OPER
	                                   AND A.FROM_TO_LOT_ID = B.FROM_TO_LOT_ID
	                                   AND A.TRAN_CODE = 'MERGE'
	                                   AND A.HIST_DEL_FLAG = ' ')
	                            GROUP BY GCT_TEST_LOT_ID)
	                 GROUP BY GCT_TEST_LOT_ID)
	        GROUP BY GCT_TEST_LOT_ID) H
	     , (SELECT LOT_CMF_4 AS GCT_TEST_LOT_ID 
	             , SUM(CASE OPER WHEN 'T1100' THEN QTY_1 ELSE 0 END) AS VM 
	             , SUM(CASE OPER WHEN 'T1080' THEN QTY_1 ELSE 0 END) AS BAKE 
	             , SUM(CASE OPER WHEN 'T1200' THEN QTY_1 ELSE 0 END) AS TNR 
	             , SUM(CASE OPER WHEN 'T1300' THEN QTY_1 ELSE 0 END) AS PACK 
	           FROM RWIPLOTSTS 
	         WHERE FACTORY = 'HMKT1' 
	           AND MAT_ID LIKE 'GC%' 
	           AND OPER IN ('T1100', 'T1080', 'T1200', 'T1300') 
	           AND LOT_TYPE = 'W' 
	           AND LOT_DEL_FLAG = ' ' 
	           AND QTY_1 > 0 
	           AND MAT_ID LIKE P_MAT_ID || '%' 
	           AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	         GROUP BY LOT_CMF_4) I
	     , (-- SHIP OUT 수량을 계산한다.
	        SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	             , SUM(SHIP_OUT_QTY) AS SHIP_OUT_QTY
	             , MAX(ACT_SHIP_TIME) AS ACT_SHIP_TIME
	          FROM (-- (+) SHIP OUT 된 수량을 계산한다.
	                SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                     , SUM(SHIP_OUT_QTY) AS SHIP_OUT_QTY
	                     , MAX(ACT_SHIP_TIME) AS ACT_SHIP_TIME
	                  FROM (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                             , A.SLOT_ID AS LOT_ID
	                             , A.SBOX_QTY AS SHIP_OUT_QTY
	                             , A.SHIP_TIME AS ACT_SHIP_TIME
	                          FROM MESMGR.CWIPLBOXHIS@RPTTOMES A, (SELECT LOT_ID AS LOT_ID
	                                                                    , LOT_CMF_4 AS LOT_CMF_4
	                                                                 FROM RWIPLOTSTS
	                                                                WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                  AND LOT_TYPE = 'W' 
	                                                                  AND MAT_ID LIKE 'GC%' 
	                                                                  AND MAT_ID LIKE P_MAT_ID || '%'
	                                                                  AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                         WHERE A.SLOT_ID = B.LOT_ID
	                           AND A.FACTORY IN ('HMKT1', 'CUSTOMER')
	                           AND A.CUST_CODE = 'GC'
	                           AND A.SHIP_FLAG = 'Y'
	                           AND A.MAT_ID LIKE 'GC%')
	                 GROUP BY GCT_TEST_LOT_ID
	                UNION ALL
	                -- (-) SHIP OUT된 수량중 다른 LOT에 MERGE되어 SHIP OUT된 수량을 빼기한다.
	                SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                     , -SUM(SHIP_OUT_QTY) AS SHIP_OUT_QTY
	                     , MAX(ACT_SHIP_TIME) AS ACT_SHIP_TIME
	                  FROM (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                             , CASE WHEN A.OLD_QTY_1 > A.QTY_1 THEN A.OLD_QTY_1 - A.QTY_1
	                                                               ELSE 0
	                               END AS SHIP_OUT_QTY
	                             , A.TRAN_TIME AS ACT_SHIP_TIME
	                          FROM RWIPLOTHIS A, (SELECT A.FROM_TO_LOT_ID AS MERGE_LOT
	                                                   , A.OPER AS MERGE_OPER
	                                                   , B.LOT_ID AS LOT_ID
	                                                   , B.LOT_CMF_4 AS LOT_CMF_4
	                                                FROM RWIPLOTMRG A, (SELECT B.LOT_CMF_4 AS LOT_CMF_4
	                                                                         , A.SLOT_ID AS LOT_ID
	                                                                         , A.SBOX_QTY AS SHIP_OUT_QTY
	                                                                      FROM MESMGR.CWIPLBOXHIS@RPTTOMES A, (SELECT LOT_ID AS LOT_ID
	                                                                                                                , LOT_CMF_4 AS LOT_CMF_4
	                                                                                                             FROM RWIPLOTSTS
	                                                                                                            WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                                                              AND LOT_TYPE = 'W' 
	                                                                                                              AND MAT_ID LIKE 'GC%' 
	                                                                                                              AND MAT_ID LIKE P_MAT_ID || '%'
	                                                                                                              AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                                                                     WHERE A.SLOT_ID = B.LOT_ID
	                                                                       AND A.FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                       AND A.SLOT_SEQ LIKE 'A%'
	                                                                       AND A.CUST_CODE = 'GC'
	                                                                       AND A.SHIP_FLAG = 'Y'
	                                                                       AND A.MAT_ID LIKE 'GC%') B
	                                               WHERE A.LOT_ID = B.LOT_ID
	                                                 AND A.FACTORY = 'HMKT1' 
	                                                 AND A.OPER = 'T1300'
	                                                 AND A.FROM_TO_FLAG = 'T'
	                                                 AND A.HIST_DEL_FLAG = ' '
	                                                 AND A.MAT_ID LIKE 'GC%') B
	                         WHERE A.LOT_ID = B.MERGE_LOT
	                           AND A.OPER = B.MERGE_OPER
	                           AND A.FROM_TO_LOT_ID = B.LOT_ID
	                           AND A.LOT_CMF_4 <> B.LOT_CMF_4
	                           AND A.TRAN_CODE = 'MERGE'
	                           AND A.HIST_DEL_FLAG = ' ')
	                 GROUP BY GCT_TEST_LOT_ID
	                UNION ALL
	                -- (+) SHIP OUT된 수량중 다른 LOT에 MERGE되어 SHIP OUT된 수량을 더하기 한다.
	                SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                     , SUM(SHIP_OUT_QTY) AS SHIP_OUT_QTY
	                     , MAX(ACT_SHIP_TIME) AS ACT_SHIP_TIME
	                  FROM (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                             , CASE WHEN A.OLD_QTY_1 > A.QTY_1 THEN A.OLD_QTY_1 - A.QTY_1
	                                                               ELSE 0
	                               END AS SHIP_OUT_QTY
	                             , A.TRAN_TIME AS ACT_SHIP_TIME
	                          FROM RWIPLOTHIS A, (SELECT A.LOT_ID AS MERGE_LOT
	                                                   , A.OPER AS MERGE_OPER
	                                                   , B.LOT_ID AS LOT_ID
	                                                   , B.LOT_CMF_4 AS LOT_CMF_4
	                                                FROM RWIPLOTMRG A, (SELECT B.LOT_CMF_4 AS LOT_CMF_4
	                                                                         , A.SLOT_ID AS LOT_ID
	                                                                         , A.SBOX_QTY AS SHIP_OUT_QTY
	                                                                      FROM MESMGR.CWIPLBOXHIS@RPTTOMES A, (SELECT LOT_ID AS LOT_ID
	                                                                                                                , LOT_CMF_4 AS LOT_CMF_4
	                                                                                                             FROM RWIPLOTSTS
	                                                                                                            WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                                                              AND LOT_TYPE = 'W'
	                                                                                                              AND MAT_ID LIKE 'GC%'
	                                                                                                              AND MAT_ID LIKE P_MAT_ID || '%'
	                                                                                                              AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                                                                     WHERE A.SLOT_ID = B.LOT_ID
	                                                                       AND A.FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                       AND A.SLOT_SEQ LIKE 'A%'
	                                                                       AND A.CUST_CODE = 'GC'
	                                                                       AND A.SHIP_FLAG = 'Y'
	                                                                       AND A.MAT_ID LIKE 'GC%') B
	                                               WHERE A.LOT_ID = B.LOT_ID
	                                                 AND A.FACTORY = 'HMKT1' 
	                                                 AND A.OPER = 'T1300'
	                                                 AND A.FROM_TO_FLAG = 'F'
	                                                 AND A.HIST_DEL_FLAG = ' '
	                                                 AND A.MAT_ID LIKE 'GC%') B
	                         WHERE A.LOT_ID = B.MERGE_LOT
	                           AND A.OPER = B.MERGE_OPER
	                           AND A.LOT_ID = B.LOT_ID
	                           AND A.LOT_CMF_4 = B.LOT_CMF_4
	                           AND A.TRAN_CODE = 'MERGE'
	                           AND A.HIST_DEL_FLAG = ' ')
	                 GROUP BY GCT_TEST_LOT_ID
	                UNION ALL
	                -- (+) 출하이력이 없는 LOT들이 다른 LOT에 MERGE되어 출하된 수량을 더하기 한다.
	                SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                     , SUM(SHIP_QTY) AS SHIP_QTY
	                     , MAX(ACT_SHIP_TIME) AS ACT_SHIP_TIME
	                  FROM (SELECT A.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                             , CASE WHEN A.OLD_QTY_1 > A.QTY_1 THEN A.OLD_QTY_1 - A.QTY_1
	                                                               ELSE 0
	                               END AS SHIP_QTY
	                             , A.TRAN_TIME AS ACT_SHIP_TIME
	                          FROM RWIPLOTHIS A, (SELECT A.FROM_TO_LOT_ID AS MERGE_LOT
	                                                   , A.OPER AS MERGE_OPER
	                                                   , B.LOT_ID AS LOT_ID
	                                                   , B.LOT_CMF_4 AS LOT_CMF_4
	                                                FROM RWIPLOTMRG A, (SELECT B.LOT_CMF_4 AS LOT_CMF_4
	                                                                         , A.SLOT_ID AS LOT_ID
	                                                                         , A.SBOX_QTY AS SHIP_OUT_QTY
	                                                                      FROM MESMGR.CWIPLBOXHIS@RPTTOMES A, (SELECT LOT_ID AS LOT_ID
	                                                                                                                , LOT_CMF_4 AS LOT_CMF_4
	                                                                                                             FROM RWIPLOTSTS
	                                                                                                            WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                                                              AND LOT_TYPE = 'W' 
	                                                                                                              AND MAT_ID LIKE 'GC%' 
	                                                                                                              AND MAT_ID LIKE P_MAT_ID || '%'
	                                                                                                              AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                                                                     WHERE A.SLOT_ID = B.LOT_ID
	                                                                       AND A.FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                       AND A.SLOT_SEQ LIKE 'A%'
	                                                                       AND A.CUST_CODE = 'GC'
	                                                                       AND A.SHIP_FLAG = 'Y'
	                                                                       AND A.MAT_ID LIKE 'GC%') B
	                                               WHERE A.LOT_ID = B.LOT_ID
	                                                 AND A.FACTORY = 'HMKT1' 
	                                                 AND A.OPER = 'T1300'
	                                                 AND A.FROM_TO_FLAG = 'T'
	                                                 AND A.HIST_DEL_FLAG = ' '
	                                                 AND A.MAT_ID LIKE 'GC%') B
	                         WHERE A.LOT_ID = B.MERGE_LOT
	                           AND A.OPER = B.MERGE_OPER
	                           AND A.FROM_TO_LOT_ID = B.LOT_ID
	                           AND A.LOT_CMF_4 <> B.LOT_CMF_4
	                           AND A.TRAN_CODE = 'MERGE'
	                           AND A.HIST_DEL_FLAG = ' ')
	                 GROUP BY GCT_TEST_LOT_ID)
	         GROUP BY GCT_TEST_LOT_ID) J 
	     , (SELECT A.GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID 
	             , A.PR_NO AS PR_NO 
	             , B.GCT_PROJECT_NO AS GCT_PROJECT_NO 
	             , C.WFR_LOT_ID AS WFR_LOT_ID 
	          FROM (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID 
	                     , A.ATTR_VALUE AS PR_NO 
	                  FROM MESMGR.MATRNAMSTS@RPTTOMES A, (SELECT DISTINCT LOT_CMF_4
	                                                        FROM (SELECT LOT_CMF_4 AS LOT_CMF_4
	                                                                FROM RWIPLOTSTS 
	                                                               WHERE FACTORY = 'HMKT1' 
	                                                                 AND MAT_ID LIKE 'GC%' 
	                                                                 AND LOT_TYPE = 'W' 
	                                                                 AND LOT_DEL_FLAG = ' ' 
	                                                                 AND QTY_1 > 0 
	                                                                 AND MAT_ID LIKE P_MAT_ID || '%' 
	                                                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	                                                              UNION ALL
	                                                              SELECT A.LOT_CMF_4 AS LOT_CMF_4
	                                                                FROM RWIPLOTSTS A, (SELECT A.FROM_TO_LOT_ID AS LOT_ID
	                                                                                      FROM RWIPLOTMRG A, (SELECT LOT_ID AS LOT_ID
	                                                                                                               , LOT_CMF_4 AS LOT_CMF_4
	                                                                                                            FROM RWIPLOTSTS
	                                                                                                           WHERE FACTORY = 'HMKT1'
	                                                                                                             AND MAT_ID LIKE 'GC%' 
	                                                                                                             AND LOT_TYPE = 'W' 
	                                                                                                             AND LOT_DEL_FLAG = ' '
	                                                                                                             AND QTY_1 > 0 
	                                                                                                             AND MAT_ID LIKE P_MAT_ID || '%' 
	                                                                                                             AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' ) B
	                                                                                     WHERE A.LOT_ID = B.LOT_ID
	                                                                                       AND A.FACTORY = 'HMKT1' 
	                                                                                       AND A.OPER = 'T1300'
	                                                                                       AND A.MAT_ID LIKE 'GC%' 
	                                                                                       AND A.HIST_DEL_FLAG = ' ') B
	                                                              WHERE A.LOT_ID = B.LOT_ID)) B 
	                 WHERE A.ATTR_KEY = B.LOT_CMF_4 
	                   AND A.FACTORY = 'HMKT1' 
	                   AND A.ATTR_TYPE = 'LOT_GCT' 
	                   AND A.ATTR_NAME = 'PRNO') A 
	             , (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID 
	                     , A.ATTR_VALUE AS GCT_PROJECT_NO 
	                  FROM MESMGR.MATRNAMSTS@RPTTOMES A, (SELECT DISTINCT LOT_CMF_4
	                                                        FROM (SELECT LOT_CMF_4 AS LOT_CMF_4
	                                                                FROM RWIPLOTSTS 
	                                                               WHERE FACTORY = 'HMKT1' 
	                                                                 AND MAT_ID LIKE 'GC%' 
	                                                                 AND LOT_TYPE = 'W' 
	                                                                 AND LOT_DEL_FLAG = ' ' 
	                                                                 AND QTY_1 > 0 
	                                                                 AND MAT_ID LIKE P_MAT_ID || '%' 
	                                                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	                                                              UNION ALL
	                                                              SELECT A.LOT_CMF_4 AS LOT_CMF_4
	                                                                FROM RWIPLOTSTS A, (SELECT A.FROM_TO_LOT_ID AS LOT_ID
	                                                                                      FROM RWIPLOTMRG A, (SELECT LOT_ID AS LOT_ID
	                                                                                                               , LOT_CMF_4 AS LOT_CMF_4
	                                                                                                            FROM RWIPLOTSTS
	                                                                                                           WHERE FACTORY = 'HMKT1'
	                                                                                                             AND MAT_ID LIKE 'GC%' 
	                                                                                                             AND LOT_TYPE = 'W' 
	                                                                                                             AND LOT_DEL_FLAG = ' '
	                                                                                                             AND QTY_1 > 0 
	                                                                                                             AND MAT_ID LIKE P_MAT_ID || '%' 
	                                                                                                             AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' ) B
	                                                                                     WHERE A.LOT_ID = B.LOT_ID
	                                                                                       AND A.FACTORY = 'HMKT1' 
	                                                                                       AND A.OPER = 'T1300'
	                                                                                       AND A.MAT_ID LIKE 'GC%' 
	                                                                                       AND A.HIST_DEL_FLAG = ' ') B
	                                                              WHERE A.LOT_ID = B.LOT_ID)) B 
	                 WHERE A.ATTR_KEY = B.LOT_CMF_4 
	                   AND A.FACTORY = 'HMKT1' 
	                   AND A.ATTR_TYPE = 'LOT_GCT' 
	                   AND A.ATTR_NAME = 'PROJECT_NO') B 
	             , (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID 
	                     , A.ATTR_VALUE AS WFR_LOT_ID 
	                  FROM MESMGR.MATRNAMSTS@RPTTOMES A, (SELECT DISTINCT LOT_CMF_4
	                                                        FROM (SELECT LOT_CMF_4 AS LOT_CMF_4
	                                                                FROM RWIPLOTSTS 
	                                                               WHERE FACTORY = 'HMKT1' 
	                                                                 AND MAT_ID LIKE 'GC%' 
	                                                                 AND LOT_TYPE = 'W' 
	                                                                 AND LOT_DEL_FLAG = ' ' 
	                                                                 AND QTY_1 > 0 
	                                                                 AND MAT_ID LIKE P_MAT_ID || '%' 
	                                                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	                                                              UNION ALL
	                                                              SELECT A.LOT_CMF_4 AS LOT_CMF_4
	                                                                FROM RWIPLOTSTS A, (SELECT A.FROM_TO_LOT_ID AS LOT_ID
	                                                                                      FROM RWIPLOTMRG A, (SELECT LOT_ID AS LOT_ID
	                                                                                                               , LOT_CMF_4 AS LOT_CMF_4
	                                                                                                            FROM RWIPLOTSTS
	                                                                                                           WHERE FACTORY = 'HMKT1'
	                                                                                                             AND MAT_ID LIKE 'GC%' 
	                                                                                                             AND LOT_TYPE = 'W' 
	                                                                                                             AND LOT_DEL_FLAG = ' '
	                                                                                                             AND QTY_1 > 0 
	                                                                                                             AND MAT_ID LIKE P_MAT_ID || '%' 
	                                                                                                             AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' ) B
	                                                                                     WHERE A.LOT_ID = B.LOT_ID
	                                                                                       AND A.FACTORY = 'HMKT1' 
	                                                                                       AND A.OPER = 'T1300'
	                                                                                       AND A.MAT_ID LIKE 'GC%' 
	                                                                                       AND A.HIST_DEL_FLAG = ' ') B
	                                                              WHERE A.LOT_ID = B.LOT_ID)) B 
	                 WHERE A.ATTR_KEY = B.LOT_CMF_4 
	                   AND A.FACTORY = 'HMKT1' 
	                   AND A.ATTR_TYPE = 'LOT_GCT' 
	                   AND A.ATTR_NAME = 'FAB_LOT_ID') C 
	         WHERE A.GCT_TEST_LOT_ID = B.GCT_TEST_LOT_ID(+) 
	           AND A.GCT_TEST_LOT_ID = C.GCT_TEST_LOT_ID(+)) K 
	 WHERE A.GCT_TEST_LOT_ID = B.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = C.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = D.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = E.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = F.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = G.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = H.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = I.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = J.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = K.GCT_TEST_LOT_ID(+) 
	 ORDER BY A.GCT_TEST_LOT_ID ASC;

	PACKAGE_RESULT := PROCEDURE_RESULT;	
	
	-- 2012-06-22-정비재 : LOG기록을 위하여 PROC 종료시간을 가져온다.
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


/************************************************************************************************************************************************************************************/
/************************************************************************************************************************************************************************************/


PROCEDURE PROC_PRD010103_T
(
		P_MAT_ID		IN		VARCHAR2,			-- 제품코드
		P_LOT_TYPE		IN		VARCHAR2			-- LOT TYPE
)
/************************************************************************************************************
 COMMENT : GCT WIP REPORT를 RWIPLOTGCT TABLE에 저장한다.
 
 BEGIN
     PACKAGE_PRD010103.PROC_PRD010103_T('%', '%');
 END;
 ************************************************************************************************************/
IS 
	-- 2012-06-22-정비재 : LOG기록을 위한 변수를 선언한다.
	P_PROC_NAME			VARCHAR2(30);
	P_PROC_DESC			VARCHAR2(50);
	P_START_TIME		VARCHAR2(14);
	P_END_TIME			VARCHAR2(14);
	P_EXE_COUNT			NUMBER;
    P_ERR_COUNT			NUMBER;
BEGIN
	
	-- 2012-06-22-정비재 : LOG기록을 위하여 변수에 데이터를 입력한다.
	P_PROC_NAME := 'PROC_PRD010103_T';
    P_PROC_DESC	:= 'MAT_ID:' || P_MAT_ID || ',LOT_TYPE:' || P_LOT_TYPE;
    P_START_TIME := TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS');
    P_END_TIME := ' ';
    P_EXE_COUNT	:= 0;
    P_ERR_COUNT := 0;
    
	-- LOG 기록
	-- PROC를 시작할 때 LOG기록을 한 번 한다.(PROC의 RUNNING TIME을 CHECK하기 위하여)
	MESMGR.SET_INFO_STS_LOG@RPTTOMES(P_PROC_NAME, P_PROC_DESC, P_START_TIME, P_START_TIME, P_EXE_COUNT, P_ERR_COUNT, 'S');
	
	
	-- 2011-07-12-정비재 : GCT WIP REPORT 데이터를 입력하기 전에 TABLE을 초기화 한다.
	DELETE FROM RWIPLOTGCT;
	COMMIT;
	
	-- 2011-07-12-정비재 : TABLE에 데이터를 입력하는 QUERY문을 실행한다.
	INSERT INTO RWIPLOTGCT(GCT_PROJECT_NO, GCT_DEVICE, PACKAGE, LD_COUNT, DIMENSION, VENDOR_PART_NO, WFR_LOT_ID, GCT_ASSY_LOT_ID, GCT_TEST_LOT_ID, VENDOR_LOT_ID
						 , PROD_RLSESCHED_NO, VENDOR_ORDER_NO, TEST_RCVD_QTY, TEST_RCVD_DATE, TEST_QUE, TEST_START_DATE, FINAL_TEST_CAS_OS, QA_EL_TEST, TEST_HOLD
						 , LOT_HOLD_DATE, LOT_HOLD_REL_DATE, TEST_OUT_QTY, TEST_OUT_FCST_DATE, TEST_OUT_DATE, VM, BAKE, TNR, PACK, PACK_OUT_QTY, PACK_OUT_DATE
						 , FGS_QTY, SHIP_OUT_QTY, FCST_SHIP_DATE, ACT_SHIP_TIME, CUST_REQ_DATE, PR_NO, TEST_PO, HOLD_CODE, REJECT, REMARK, BATCH_TIME)
	SELECT NVL(K.GCT_PROJECT_NO, ' ') AS GCT_PROJECT_NO 
	     , NVL(A.GCT_DEVICE, ' ') AS GCT_DEVICE 
	     , NVL(A.PACKAGE, ' ') AS PACKAGE 
	     , NVL(A.LD_COUNT, ' ') AS LD_COUNT 
	     , NVL(A.DIMENSION, ' ') AS DIMENSION 
	     , NVL(A.VENDOR_PART_NO, ' ') AS VENDOR_PART_NO 
	     , NVL(K.WFR_LOT_ID, ' ' ) AS WFR_LOT_ID 
	     , NVL(A.GCT_ASSY_LOT_ID, ' ') AS GCT_ASSY_LOT_ID 
	     , NVL(A.GCT_TEST_LOT_ID, ' ') AS GCT_TEST_LOT_ID 
	     , NVL(A.VENDOR_LOT_ID, ' ') AS VENDOR_LOT_ID 
	     , ' ' AS PROD_RLSESCHED_NO 
	     , ' ' AS VENDOR_ORDER_NO 
	     , NVL(A.TEST_RCVD_QTY, 0) AS TEST_RCVD_QTY 
	     , CASE WHEN NVL(A.TEST_RCVD_DATE, ' ') <> ' ' THEN TO_CHAR(TO_DATE(NVL(A.TEST_RCVD_DATE, ' '), 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') 
	                                                   ELSE ' ' 
	       END AS TEST_RCVD_DATE 
	     , NVL(B.TEST_QUE, 0) AS TEST_QUE 
	     , CASE WHEN NVL(A.TEST_START_DATE, ' ') <> ' ' THEN TO_CHAR(TO_DATE(NVL(A.TEST_START_DATE, ' '), 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') 
	                                                    ELSE ' ' 
	       END AS TEST_START_DATE 
	     , NVL(C.FINAL_TEST_CAS_OS, 0) AS FINAL_TEST_CAS_OS 
	     , NVL(D.QA_EL_TEST, 0) AS QA_EL_TEST 
	     , NVL(E.TEST_HOLD, 0) AS TEST_HOLD 
	     , CASE WHEN NVL(F.LOT_HOLD_DATE, ' ') <> ' ' THEN TO_CHAR(TO_DATE(NVL(F.LOT_HOLD_DATE, ' '), 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') 
	                                                  ELSE ' ' 
	       END AS LOT_HOLD_DATE 
	     , CASE WHEN NVL(G.LOT_HOLD_REL_DATE, ' ') <> ' ' THEN TO_CHAR(TO_DATE(NVL(G.LOT_HOLD_REL_DATE, ' '), 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') 
	                                                      ELSE ' ' 
	       END AS LOT_HOLD_REL_DATE 
	     , NVL(H.TEST_OUT_QTY, 0) AS TEST_OUT_QTY 
	     , ' ' AS TEST_OUT_FCST_DATE 
	     , CASE WHEN NVL(H.TEST_OUT_DATE, ' ') <> ' ' THEN TO_CHAR(TO_DATE(NVL(H.TEST_OUT_DATE, ' '), 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') 
	                                                  ELSE ' ' 
	       END AS TEST_OUT_DATE 
	     , NVL(I.VM, 0) AS VM 
	     , NVL(I.BAKE, 0) AS BAKE 
	     , NVL(I.TNR, 0) AS TNR 
	     --, NVL(I.PACK, 0) AS PACK
	     , CASE WHEN NVL(I.PACK, 0) <= 0 THEN NVL(I.PACK, 0)
	     		ELSE NVL(I.PACK, 0) + NVL(H.PACK_OUT_QTY, 0)
	       END AS PACK
	     --, NVL(H.PACK_OUT_QTY, 0) AS PACK_OUT_QTY
		 , CASE WHEN NVL(I.PACK, 0) <= 0 THEN NVL(H.PACK_OUT_QTY, 0)
	     		ELSE 0
		   END AS PACK_OUT_QTY
	     , CASE WHEN NVL(H.PACK_OUT_DATE, ' ') <> ' ' THEN TO_CHAR(TO_DATE(NVL(H.PACK_OUT_DATE, ' '), 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') 
	                                                  ELSE ' ' 
	       END AS PACK_OUT_DATE 
	     --, NVL(H.PACK_OUT_QTY, 0) - NVL(J.SHIP_OUT_QTY, 0) AS FGS_QTY
	     , CASE WHEN NVL(I.PACK, 0) <= 0 THEN NVL(H.PACK_OUT_QTY, 0) - NVL(J.SHIP_OUT_QTY, 0)
	     		ELSE 0
	       END AS FGS_QTY
	     , NVL(J.SHIP_OUT_QTY, 0) AS SHIP_OUT_QTY 
	     , ' ' AS FCST_SHIP_DATE 
	     , CASE WHEN NVL(J.ACT_SHIP_TIME, ' ') <> ' ' THEN TO_CHAR(TO_DATE(NVL(J.ACT_SHIP_TIME, ' '), 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') 
	                                                  ELSE ' ' 
	       END AS ACT_SHIP_TIME 
	     , ' ' AS CUST_REQ_DATE 
	     , NVL(K.PR_NO, ' ') AS PR_NO 
	     , NVL(A.TEST_PO, ' ') AS TEST_PO 
	     , NVL(F.HOLD_CODE, ' ') AS HOLD_CODE 
	     --, NVL(A.TEST_RCVD_QTY, 0) - (NVL(B.TEST_QUE, 0) + NVL(D.QA_EL_TEST, 0) + NVL(C.FINAL_TEST_CAS_OS, 0) + NVL(E.TEST_HOLD, 0) + NVL(I.VM, 0) + NVL(I.BAKE, 0) + NVL(I.TNR, 0) + NVL(I.PACK, 0) + NVL(H.PACK_OUT_QTY, 0)) AS REJECT
	     , NVL(A.TEST_RCVD_QTY, 0) 
	    - (NVL(B.TEST_QUE, 0) + NVL(D.QA_EL_TEST, 0) + NVL(C.FINAL_TEST_CAS_OS, 0) + NVL(I.VM, 0) + NVL(I.BAKE, 0) + NVL(I.TNR, 0) 
	     + (CASE WHEN NVL(I.PACK, 0) <= 0 THEN NVL(I.PACK, 0) ELSE NVL(I.PACK, 0) + NVL(H.PACK_OUT_QTY, 0) END) 
	     + (CASE WHEN NVL(I.PACK, 0) <= 0 THEN NVL(H.PACK_OUT_QTY, 0) ELSE 0 END)) AS REJECT
	     , A.REMARK AS REMARK
	     , SUBSTR(TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS'), 1, 10) || '0000' AS BATCH_TIME
	  FROM (SELECT A.GCT_ASSY_LOT_ID AS GCT_ASSY_LOT_ID 
	             , A.GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID 
	             , A.VENDOR_LOT_ID AS VENDOR_LOT_ID 
	             , A.VENDOR_PART_NO AS VENDOR_PART_NO 
	             , A.TEST_RCVD_QTY AS TEST_RCVD_QTY 
	             , A.TEST_RCVD_DATE AS TEST_RCVD_DATE 
	             , A.TEST_START_DATE AS TEST_START_DATE 
	             , B.GCT_DEVICE AS GCT_DEVICE 
	             , B.PACKAGE AS PACKAGE 
	             , B.LD_COUNT AS LD_COUNT 
	             , B.DIMENSION AS DIMENSION 
	             , A.TEST_PO AS TEST_PO 
	             , A.REMARK AS REMARK 
	          FROM (SELECT B.GCT_ASSY_LOT_ID AS GCT_ASSY_LOT_ID 
	                     , B.GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID 
	                     , B.VENDOR_LOT_ID AS VENDOR_LOT_ID 
	                     , B.VENDOR_PART_NO AS VENDOR_PART_NO 
	                     , A.CREATE_QTY_1 AS TEST_RCVD_QTY 
	                     , B.TEST_RCVD_DATE AS TEST_RCVD_DATE 
	                     , B.TEST_START_DATE AS TEST_START_DATE 
	                     , B.TEST_PO AS TEST_PO 
	                     , B.REMARK AS REMARK 
	                  FROM RWIPLOTSTS A, (SELECT GCT_ASSY_LOT_ID AS GCT_ASSY_LOT_ID
	                                           , GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                                           , VENDOR_LOT_ID AS VENDOR_LOT_ID
	                                           , MAX(VENDOR_PART_NO) AS VENDOR_PART_NO
	                                           , MIN(TEST_RCVD_DATE) AS TEST_RCVD_DATE 
	                                           , MIN(TEST_START_DATE) AS TEST_START_DATE 
	                                           , MAX(TEST_PO) AS TEST_PO 
	                                           , MAX(REMARK) AS REMARK
	                                        FROM (SELECT SUBSTR(LOT_CMF_4, 1, 9) || '0' AS GCT_ASSY_LOT_ID 
	                                                   , LOT_CMF_4 AS GCT_TEST_LOT_ID 
	                                                   , LOT_CMF_4 AS VENDOR_LOT_ID 
	                                                   , MAT_ID AS VENDOR_PART_NO 
	                                                   , MIN(CREATE_TIME) AS TEST_RCVD_DATE 
	                                                   , MIN(OPER_IN_TIME) AS TEST_START_DATE 
	                                                   , MAX(LOT_CMF_3) AS TEST_PO 
	                                                   , MAX(LAST_COMMENT) AS REMARK 
	                                                FROM RWIPLOTSTS 
	                                               WHERE FACTORY = 'HMKT1' 
	                                                 AND MAT_ID LIKE 'GC%'
	                                                 AND LOT_TYPE = 'W' 
	                                                 AND LOT_DEL_FLAG = ' ' 
	                                                 AND QTY_1 > 0 
	                                                 AND MAT_ID LIKE P_MAT_ID || '%' 
	                                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	                                               GROUP BY SUBSTR(LOT_CMF_4, 1, 9) || '0', LOT_CMF_4, MAT_ID
	                                              UNION ALL
	                                              SELECT SUBSTR(A.LOT_CMF_4, 1, 9) || '0' AS GCT_ASSY_LOT_ID 
	                                                   , A.LOT_CMF_4 AS GCT_TEST_LOT_ID 
	                                                   , A.LOT_CMF_4 AS VENDOR_LOT_ID 
	                                                   , A.MAT_ID AS VENDOR_PART_NO 
	                                                   , MIN(A.CREATE_TIME) AS TEST_RCVD_DATE 
	                                                   , MIN(A.OPER_IN_TIME) AS TEST_START_DATE 
	                                                   , MAX(A.LOT_CMF_3) AS TEST_PO 
	                                                   , MAX(A.LAST_COMMENT) AS REMARK
	                                                FROM RWIPLOTHIS A, (SELECT A.FROM_TO_LOT_ID AS MERGE_LOT
	                                                                         , A.OPER AS MERGE_OPER
	                                                                         , B.LOT_ID AS LOT_ID
	                                                                         , B.LOT_CMF_4 AS LOT_CMF_4
	                                                                      FROM RWIPLOTMRG A, (SELECT LOT_ID AS LOT_ID
	                                                                                               , LOT_CMF_4  AS LOT_CMF_4
	                                                                                            FROM RWIPLOTSTS
	                                                                                           WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                                             AND MAT_ID LIKE 'GC%' 
	                                                                                             AND LOT_TYPE = 'W' 
	                                                                                             AND LOT_DEL_FLAG = ' '
	                                                                                             AND QTY_1 > 0 
	                                                                                             AND MAT_ID LIKE P_MAT_ID || '%' 
	                                                                                             AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' ) B
	                                                                     WHERE A.LOT_ID = B.LOT_ID
	                                                                       AND A.FACTORY = 'HMKT1' 
	                                                                       AND A.OPER = 'T1300'
	                                                                       AND A.MAT_ID LIKE 'GC%' 
	                                                                       AND A.HIST_DEL_FLAG = ' ') B
	                                               WHERE A.LOT_ID = B.MERGE_LOT
	                                                 AND A.OPER = B.MERGE_OPER
	                                                 AND A.TRAN_CODE = 'MERGE'
	                                               GROUP BY SUBSTR(A.LOT_CMF_4, 1, 9) || '0', A.LOT_CMF_4, A.LOT_CMF_4, A.MAT_ID)
	                                        GROUP BY GCT_ASSY_LOT_ID, GCT_TEST_LOT_ID, VENDOR_LOT_ID) B 
	                 WHERE B.GCT_TEST_LOT_ID = A.LOT_ID(+)) A 
	             , (SELECT MAT_ID AS VENDOR_PART_NO 
	                     , MAT_CMF_7 AS GCT_DEVICE 
	                     , MAT_GRP_3 AS PACKAGE 
	                     , MAT_GRP_6 AS LD_COUNT 
	                     , MAT_CMF_9 AS DIMENSION 
	                  FROM MWIPMATDEF 
	                 WHERE FACTORY = 'HMKT1' 
	                   AND MAT_GRP_1 = 'GC' 
	                   AND DELETE_FLAG = ' ') B 
	        WHERE A.VENDOR_PART_NO = B.VENDOR_PART_NO(+)) A 
	     , (SELECT LOT_CMF_4 AS GCT_TEST_LOT_ID 
	             , SUM(QTY_1) AS TEST_QUE 
	          FROM RWIPLOTSTS 
	         WHERE FACTORY = 'HMKT1' 
	           AND MAT_ID LIKE 'GC%' 
	           AND OPER = 'T0000' 
	           AND LOT_TYPE = 'W' 
	           AND LOT_DEL_FLAG = ' ' 
	           AND QTY_1 > 0 
	           AND MAT_ID LIKE P_MAT_ID || '%' 
	           AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	         GROUP BY LOT_CMF_4) B 
	     , (SELECT LOT_CMF_4 AS GCT_TEST_LOT_ID 
	             , SUM(QTY_1) AS FINAL_TEST_CAS_OS 
	          FROM RWIPLOTSTS 
	         WHERE FACTORY = 'HMKT1' 
	           AND MAT_ID LIKE 'GC%' 
	           AND OPER IN ('T0090', 'T0100', 'T0650', 'T0900') 
	           AND LOT_TYPE = 'W' 
	           AND LOT_DEL_FLAG = ' ' 
	           AND HOLD_FLAG = ' ' 
	           AND QTY_1 > 0 
	           AND MAT_ID LIKE P_MAT_ID || '%' 
	           AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	         GROUP BY LOT_CMF_4) C 
	     , (SELECT LOT_CMF_4 AS GCT_TEST_LOT_ID 
	             , SUM(QTY_1) AS QA_EL_TEST 
	          FROM RWIPLOTSTS 
	         WHERE FACTORY = 'HMKT1' 
	           AND MAT_ID LIKE 'GC%' 
	           AND OPER IN ('T0300', 'T1040') 
	           AND LOT_TYPE = 'W' 
	           AND LOT_DEL_FLAG = ' ' 
	           AND QTY_1 > 0 
	           AND MAT_ID LIKE P_MAT_ID || '%' 
	           AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	         GROUP BY LOT_CMF_4) D 
	     , (SELECT LOT_CMF_4 AS GCT_TEST_LOT_ID 
	             , SUM(QTY_1) AS TEST_HOLD  
	          FROM RWIPLOTSTS 
	         WHERE FACTORY = 'HMKT1' 
	           AND MAT_ID LIKE 'GC%' 
	           AND OPER IN ('T0090', 'T0100', 'T0650', 'T0900') 
	           AND LOT_TYPE = 'W' 
	           AND HOLD_FLAG = 'Y' 
	           AND LOT_DEL_FLAG = ' ' 
	           AND QTY_1 > 0 
	           AND MAT_ID LIKE P_MAT_ID || '%' 
	           AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	         GROUP BY LOT_CMF_4) E 
	     , (SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	             , LOT_HOLD_DATE AS LOT_HOLD_DATE
	             , HOLD_CODE AS HOLD_CODE
	          FROM (SELECT LOT_CMF_4 AS GCT_TEST_LOT_ID 
	                     , MAX(LAST_TRAN_TIME) AS LOT_HOLD_DATE 
	                     , HOLD_CODE AS HOLD_CODE
	                  FROM RWIPLOTSTS 
	                 WHERE FACTORY = 'HMKT1' 
	                   AND MAT_ID LIKE 'GC%' 
	                   AND LOT_TYPE = 'W' 
	                   AND HOLD_FLAG = 'Y' 
	                   AND LOT_DEL_FLAG = ' ' 
	                   AND QTY_1 > 0 
	                   AND MAT_ID LIKE P_MAT_ID || '%' 
	                   AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	                 GROUP BY LOT_CMF_4, HOLD_CODE
	                 ORDER BY LOT_HOLD_DATE DESC)
	         WHERE ROWNUM = 1) F 
	     , (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID 
	             , MAX(A.RELEASE_TRAN_TIME) AS LOT_HOLD_REL_DATE 
	          FROM RWIPLOTHLD A, (SELECT LOT_ID AS LOT_ID
	                                   , LOT_CMF_4 AS LOT_CMF_4
	                                FROM RWIPLOTSTS 
	                               WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                 AND MAT_ID LIKE 'GC%' 
	                                 AND LOT_TYPE = 'W' 
	                                 AND LOT_DEL_FLAG = ' ' 
	                                 AND QTY_1 > 0 
	                                 AND MAT_ID LIKE P_MAT_ID || '%' 
	                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' ) B 
	         WHERE A.LOT_ID = B.LOT_ID 
	           AND A.FACTORY = 'HMKT1' 
	           AND A.MAT_ID LIKE 'GC%' 
	           AND A.HIST_DEL_FLAG = ' ' 
	         GROUP BY LOT_CMF_4) G 
	     , (SELECT GCT_TEST_LOT_ID
	             , MAX(TEST_OUT_DATE) AS TEST_OUT_DATE
	             , SUM(TEST_OUT_QTY) AS TEST_OUT_QTY
	             , MAX(PACK_OUT_DATE) AS PACK_OUT_DATE
	             , SUM(PACK_OUT_QTY) AS PACK_OUT_QTY
	          FROM (SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                     , TEST_OUT_DATE AS TEST_OUT_DATE
	                     , SUM(TEST_OUT_QTY) AS TEST_OUT_QTY
	                     , MAX(PACK_OUT_DATE) AS PACK_OUT_DATE 
	                     , SUM(PACK_OUT_QTY) AS PACK_OUT_QTY
	                  FROM (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                             , MAX(A.TRAN_TIME) AS TEST_OUT_DATE
	                             , SUM(A.QTY_1) AS TEST_OUT_QTY
	                             , ' ' AS PACK_OUT_DATE
	                             , 0 AS PACK_OUT_QTY
	                          FROM RWIPLOTHIS A, (SELECT LOT_ID AS LOT_ID
	                                                   , LOT_CMF_4 AS LOT_CMF_4
	                                                FROM RWIPLOTSTS
	                                               WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                 AND MAT_ID LIKE 'GC%' 
	                                                 AND LOT_TYPE = 'W' 
	                                                 AND MAT_ID LIKE P_MAT_ID || '%'
	                                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                         WHERE A.LOT_ID = B.LOT_ID
	                           AND A.FACTORY = 'HMKT1'
	                           AND A.OLD_OPER = 'T1040'
	                           AND A.TRAN_CODE = 'END'
	                           AND A.RWK_FLAG = 'Y'
	                           AND A.HIST_DEL_FLAG = ' '
	                         GROUP BY B.LOT_CMF_4
	                        UNION ALL
	                        SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                             , MAX(A.TRAN_TIME) AS TEST_OUT_DATE
	                             , SUM(A.QTY_1) AS TEST_OUT_QTY
	                             , ' ' AS PACK_OUT_DATE
	                             , 0 AS PACK_OUT_QTY
	                          FROM RWIPLOTHIS A, (SELECT LOT_ID AS LOT_ID
	                                                   , LOT_CMF_4 AS LOT_CMF_4
	                                                FROM RWIPLOTSTS
	                                               WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                 AND MAT_ID LIKE 'GC%' 
	                                                 AND LOT_TYPE = 'W' 
	                                                 AND MAT_ID LIKE P_MAT_ID || '%'
	                                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                         WHERE A.LOT_ID = B.LOT_ID
	                           AND A.FACTORY = 'HMKT1'
	                           AND A.OLD_OPER = 'T1040'
	                           AND A.TRAN_CODE = 'END'
	                           AND A.RWK_FLAG = ' '
	                           AND A.HIST_DEL_FLAG = ' '
	                           AND A.LOT_ID NOT IN (SELECT A.LOT_ID
	                                                  FROM RWIPLOTHIS A, (SELECT LOT_ID AS LOT_ID
	                                                                           , LOT_CMF_4 AS LOT_CMF_4
	                                                                        FROM RWIPLOTSTS
	                                                                       WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                         AND MAT_ID LIKE 'GC%' 
	                                                                         AND LOT_TYPE = 'W' 
	                                                                         AND MAT_ID LIKE P_MAT_ID || '%'
	                                                                         AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                                                 WHERE A.LOT_ID = B.LOT_ID
	                                                   AND A.FACTORY = 'HMKT1'
	                                                   AND A.OLD_OPER = 'T1040'
	                                                   AND A.TRAN_CODE = 'END'
	                                                   AND A.RWK_FLAG = 'Y'
	                                                   AND A.HIST_DEL_FLAG = ' ')
	                         GROUP BY B.LOT_CMF_4)
	                 GROUP BY GCT_TEST_LOT_ID, TEST_OUT_DATE
	                UNION ALL
	                -- PACK_OUT_QTY를 계산하는 QUERY
	                SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                     , MAX(TEST_OUT_DATE) AS TEST_OUT_DATE
	                     , SUM(TEST_OUT_QTY) AS TEST_OUT_QTY
	                     , MAX(PACK_OUT_DATE) AS PACK_OUT_DATE
	                     , SUM(PACK_OUT_QTY) AS PACK_OUT_QTY 
	                  FROM (-- (+) REWORK으로 PACK OUT 된 수량
	                        SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                             , ' ' AS TEST_OUT_DATE
	                             , 0 AS TEST_OUT_QTY
	                             , MAX(A.TRAN_TIME) AS PACK_OUT_DATE
	                             , SUM(A.QTY_1) AS PACK_OUT_QTY
	                          FROM RWIPLOTHIS A, (SELECT LOT_ID AS LOT_ID
	                                                   , LOT_CMF_4 AS LOT_CMF_4
	                                                FROM RWIPLOTSTS
	                                               WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                 AND MAT_ID LIKE 'GC%' 
	                                                 AND LOT_TYPE = 'W' 
	                                                 AND MAT_ID LIKE P_MAT_ID || '%'
	                                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                         WHERE A.LOT_ID = B.LOT_ID
	                           AND A.FACTORY = 'HMKT1'
	                           AND A.OLD_OPER = 'T1300'
	                           AND A.TRAN_CODE = 'END'
	                           AND A.RWK_FLAG = 'Y'
	                           AND A.HIST_DEL_FLAG = ' '
	                         GROUP BY B.LOT_CMF_4
	                        UNION ALL
	                        -- (+) 정상적인 작업은 PACK OUT 된 수량
	                        SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                             , ' ' AS TEST_OUT_DATE
	                             , 0 AS TEST_OUT_QTY
	                             , MAX(A.TRAN_TIME) AS PACK_OUT_DATE
	                             , SUM(A.QTY_1) AS PACK_OUT_QTY
	                          FROM RWIPLOTHIS A, (SELECT LOT_ID AS LOT_ID
	                                                   , LOT_CMF_4 AS LOT_CMF_4
	                                                FROM RWIPLOTSTS
	                                               WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                 AND MAT_ID LIKE 'GC%' 
	                                                 AND LOT_TYPE = 'W' 
	                                                 AND MAT_ID LIKE P_MAT_ID || '%'
	                                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                         WHERE A.LOT_ID = B.LOT_ID
	                           AND A.FACTORY = 'HMKT1'
	                           AND A.OLD_OPER = 'T1300'
	                           AND A.TRAN_CODE = 'END'
	                           AND A.RWK_FLAG = ' '
	                           AND A.HIST_DEL_FLAG = ' '
	                           AND A.LOT_ID NOT IN (SELECT A.LOT_ID
	                                                  FROM RWIPLOTHIS A, (SELECT LOT_ID AS LOT_ID
	                                                                           , LOT_CMF_4 AS LOT_CMF_4
	                                                                        FROM RWIPLOTSTS
	                                                                       WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                         AND MAT_ID LIKE 'GC%' 
	                                                                         AND LOT_TYPE = 'W' 
	                                                                         AND MAT_ID LIKE P_MAT_ID || '%'
	                                                                         AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                                                 WHERE A.LOT_ID = B.LOT_ID
	                                                   AND A.FACTORY = 'HMKT1'
	                                                   AND A.OLD_OPER = 'T1300'
	                                                   AND A.TRAN_CODE = 'END'
	                                                   AND A.RWK_FLAG = 'Y'
	                                                   AND A.HIST_DEL_FLAG = ' ')
	                         GROUP BY B.LOT_CMF_4
	                        UNION ALL
	                        -- (-)본 LOT에 다른 LOT이 MERGE되어 PACK OUT 된 수량
	                        SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                             , MAX(TEST_OUT_DATE) AS TEST_OUT_DATE
	                             , SUM(TEST_OUT_QTY) AS TEST_OUT_QTY
	                             , MAX(PACK_OUT_DATE) AS PACK_OUT_DATE
	                             , -SUM(PACK_OUT_QTY) AS PACK_OUT_QTY
	                          FROM (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                                     , ' ' AS TEST_OUT_DATE
	                                     , 0 AS TEST_OUT_QTY
	                                     , ' ' AS PACK_OUT_DATE
	                                     , CASE WHEN A.OLD_QTY_1 > A.QTY_1 THEN A.OLD_QTY_1 - A.QTY_1
	                                                                       ELSE 0
	                                       END AS PACK_OUT_QTY
	                                  FROM RWIPLOTHIS A, (SELECT A.FROM_TO_LOT_ID AS MERGE_LOT
	                                                           , A.OPER AS MERGE_OPER
	                                                           , B.LOT_ID AS LOT_ID
	                                                           , B.LOT_CMF_4 AS LOT_CMF_4
	                                                        FROM RWIPLOTMRG A, (SELECT LOT_ID AS LOT_ID
	                                                                                 , LOT_CMF_4 AS LOT_CMF_4
	                                                                              FROM RWIPLOTSTS
	                                                                             WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                               AND MAT_ID LIKE 'GC%' 
	                                                                               AND LOT_TYPE = 'W' 
	                                                                               AND MAT_ID LIKE P_MAT_ID || '%'
	                                                                               AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                                                       WHERE A.LOT_ID = B.LOT_ID
	                                                         AND A.FACTORY = 'HMKT1' 
	                                                         AND A.OPER = 'T1300'
	                                                         AND A.MAT_ID LIKE 'GC%' 
	                                                         AND A.FROM_TO_FLAG = 'T'
	                                                         AND A.HIST_DEL_FLAG = ' ') B
	                                 WHERE A.LOT_ID = B.MERGE_LOT
	                                   AND A.OPER = B.MERGE_OPER
	                                   AND A.FROM_TO_LOT_ID = B.LOT_ID
	                                   AND A.LOT_CMF_4 <> B.LOT_CMF_4
	                                   AND A.TRAN_CODE = 'MERGE'
	                                   AND A.HIST_DEL_FLAG = ' ')
	                         GROUP BY GCT_TEST_LOT_ID
	                        UNION ALL
	                        -- (+)본 LOT이 다른LOT에 MERGE되어 PACK OUT된 수량
	                        SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                             , MAX(TEST_OUT_DATE) AS TEST_OUT_DATE
	                             , SUM(TEST_OUT_QTY) AS TEST_OUT_QTY
	                             , MAX(PACK_OUT_DATE) AS PACK_OUT_DATE
	                             , SUM(PACK_OUT_QTY) AS PACK_OUT_QTY
	                          FROM (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                                     , ' ' AS TEST_OUT_DATE
	                                     , 0 AS TEST_OUT_QTY
	                                     , ' ' AS PACK_OUT_DATE
	                                     , CASE WHEN A.OLD_QTY_1 > A.QTY_1 THEN A.OLD_QTY_1 - A.QTY_1
	                                                                       ELSE 0
	                                       END AS PACK_OUT_QTY
	                                  FROM RWIPLOTHIS A, (SELECT A.LOT_ID AS MERGE_LOT
	                                                           , A.OPER AS MERGE_OPER
	                                                           , A.FROM_TO_LOT_ID AS FROM_TO_LOT_ID
	                                                           , B.LOT_ID AS LOT_ID
	                                                           , B.LOT_CMF_4 AS LOT_CMF_4
	                                                        FROM RWIPLOTMRG A, (SELECT LOT_ID AS LOT_ID
	                                                                                 , LOT_CMF_4 AS LOT_CMF_4
	                                                                              FROM RWIPLOTSTS
	                                                                             WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                               AND MAT_ID LIKE 'GC%' 
	                                                                               AND LOT_TYPE = 'W' 
	                                                                               AND MAT_ID LIKE P_MAT_ID || '%'
	                                                                               AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                                                       WHERE A.LOT_ID = B.LOT_ID
	                                                         AND A.FACTORY = 'HMKT1' 
	                                                         AND A.OPER = 'T1300'
	                                                         AND A.MAT_ID LIKE 'GC%'
	                                                         AND A.FROM_TO_FLAG = 'F'
	                                                         AND A.HIST_DEL_FLAG = ' '
	                                                         AND SUBSTR(A.FROM_TO_LOT_ID, 1, LENGTH(B.LOT_CMF_4)) <> B.LOT_CMF_4) B
	                                 WHERE A.LOT_ID = B.MERGE_LOT
	                                   AND A.OPER = B.MERGE_OPER
	                                   AND A.FROM_TO_LOT_ID = B.FROM_TO_LOT_ID
	                                   AND A.TRAN_CODE = 'MERGE'
	                                   AND A.HIST_DEL_FLAG = ' ')
	                            GROUP BY GCT_TEST_LOT_ID)
	                 GROUP BY GCT_TEST_LOT_ID)
	        GROUP BY GCT_TEST_LOT_ID) H
	     , (SELECT LOT_CMF_4 AS GCT_TEST_LOT_ID 
	             , SUM(CASE OPER WHEN 'T1100' THEN QTY_1 ELSE 0 END) AS VM 
	             , SUM(CASE OPER WHEN 'T1080' THEN QTY_1 ELSE 0 END) AS BAKE 
	             , SUM(CASE OPER WHEN 'T1200' THEN QTY_1 ELSE 0 END) AS TNR 
	             , SUM(CASE OPER WHEN 'T1300' THEN QTY_1 ELSE 0 END) AS PACK 
	           FROM RWIPLOTSTS 
	         WHERE FACTORY = 'HMKT1' 
	           AND MAT_ID LIKE 'GC%' 
	           AND OPER IN ('T1100', 'T1080', 'T1200', 'T1300') 
	           AND LOT_TYPE = 'W' 
	           AND LOT_DEL_FLAG = ' ' 
	           AND QTY_1 > 0 
	           AND MAT_ID LIKE P_MAT_ID || '%' 
	           AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	         GROUP BY LOT_CMF_4) I
	     , (-- SHIP OUT 수량을 계산한다.
	        SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	             , SUM(SHIP_OUT_QTY) AS SHIP_OUT_QTY
	             , MAX(ACT_SHIP_TIME) AS ACT_SHIP_TIME
	          FROM (-- (+) SHIP OUT 된 수량을 계산한다.
	                SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                     , SUM(SHIP_OUT_QTY) AS SHIP_OUT_QTY
	                     , MAX(ACT_SHIP_TIME) AS ACT_SHIP_TIME
	                  FROM (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                             , A.SLOT_ID AS LOT_ID
	                             , A.SBOX_QTY AS SHIP_OUT_QTY
	                             , A.SHIP_TIME AS ACT_SHIP_TIME
	                          FROM MESMGR.CWIPLBOXHIS@RPTTOMES A, (SELECT LOT_ID AS LOT_ID
	                                                                    , LOT_CMF_4 AS LOT_CMF_4
	                                                                 FROM RWIPLOTSTS
	                                                                WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                  AND LOT_TYPE = 'W' 
	                                                                  AND MAT_ID LIKE 'GC%' 
	                                                                  AND MAT_ID LIKE P_MAT_ID || '%'
	                                                                  AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                         WHERE A.SLOT_ID = B.LOT_ID
	                           AND A.FACTORY IN ('HMKT1', 'CUSTOMER')
	                           AND A.CUST_CODE = 'GC'
	                           AND A.SHIP_FLAG = 'Y'
	                           AND A.MAT_ID LIKE 'GC%')
	                 GROUP BY GCT_TEST_LOT_ID
	                UNION ALL
	                -- (-) SHIP OUT된 수량중 다른 LOT에 MERGE되어 SHIP OUT된 수량을 빼기한다.
	                SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                     , -SUM(SHIP_OUT_QTY) AS SHIP_OUT_QTY
	                     , MAX(ACT_SHIP_TIME) AS ACT_SHIP_TIME
	                  FROM (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                             , CASE WHEN A.OLD_QTY_1 > A.QTY_1 THEN A.OLD_QTY_1 - A.QTY_1
	                                                               ELSE 0
	                               END AS SHIP_OUT_QTY
	                             , A.TRAN_TIME AS ACT_SHIP_TIME
	                          FROM RWIPLOTHIS A, (SELECT A.FROM_TO_LOT_ID AS MERGE_LOT
	                                                   , A.OPER AS MERGE_OPER
	                                                   , B.LOT_ID AS LOT_ID
	                                                   , B.LOT_CMF_4 AS LOT_CMF_4
	                                                FROM RWIPLOTMRG A, (SELECT B.LOT_CMF_4 AS LOT_CMF_4
	                                                                         , A.SLOT_ID AS LOT_ID
	                                                                         , A.SBOX_QTY AS SHIP_OUT_QTY
	                                                                      FROM MESMGR.CWIPLBOXHIS@RPTTOMES A, (SELECT LOT_ID AS LOT_ID
	                                                                                                                , LOT_CMF_4 AS LOT_CMF_4
	                                                                                                             FROM RWIPLOTSTS
	                                                                                                            WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                                                              AND LOT_TYPE = 'W' 
	                                                                                                              AND MAT_ID LIKE 'GC%' 
	                                                                                                              AND MAT_ID LIKE P_MAT_ID || '%'
	                                                                                                              AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                                                                     WHERE A.SLOT_ID = B.LOT_ID
	                                                                       AND A.FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                       AND A.SLOT_SEQ LIKE 'A%'
	                                                                       AND A.CUST_CODE = 'GC'
	                                                                       AND A.SHIP_FLAG = 'Y'
	                                                                       AND A.MAT_ID LIKE 'GC%') B
	                                               WHERE A.LOT_ID = B.LOT_ID
	                                                 AND A.FACTORY = 'HMKT1' 
	                                                 AND A.OPER = 'T1300'
	                                                 AND A.FROM_TO_FLAG = 'T'
	                                                 AND A.HIST_DEL_FLAG = ' '
	                                                 AND A.MAT_ID LIKE 'GC%') B
	                         WHERE A.LOT_ID = B.MERGE_LOT
	                           AND A.OPER = B.MERGE_OPER
	                           AND A.FROM_TO_LOT_ID = B.LOT_ID
	                           AND A.LOT_CMF_4 <> B.LOT_CMF_4
	                           AND A.TRAN_CODE = 'MERGE'
	                           AND A.HIST_DEL_FLAG = ' ')
	                 GROUP BY GCT_TEST_LOT_ID
	                UNION ALL
	                -- (+) SHIP OUT된 수량중 다른 LOT에 MERGE되어 SHIP OUT된 수량을 더하기 한다.
	                SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                     , SUM(SHIP_OUT_QTY) AS SHIP_OUT_QTY
	                     , MAX(ACT_SHIP_TIME) AS ACT_SHIP_TIME
	                  FROM (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                             , CASE WHEN A.OLD_QTY_1 > A.QTY_1 THEN A.OLD_QTY_1 - A.QTY_1
	                                                               ELSE 0
	                               END AS SHIP_OUT_QTY
	                             , A.TRAN_TIME AS ACT_SHIP_TIME
	                          FROM RWIPLOTHIS A, (SELECT A.LOT_ID AS MERGE_LOT
	                                                   , A.OPER AS MERGE_OPER
	                                                   , B.LOT_ID AS LOT_ID
	                                                   , B.LOT_CMF_4 AS LOT_CMF_4
	                                                FROM RWIPLOTMRG A, (SELECT B.LOT_CMF_4 AS LOT_CMF_4
	                                                                         , A.SLOT_ID AS LOT_ID
	                                                                         , A.SBOX_QTY AS SHIP_OUT_QTY
	                                                                      FROM MESMGR.CWIPLBOXHIS@RPTTOMES A, (SELECT LOT_ID AS LOT_ID
	                                                                                                                , LOT_CMF_4 AS LOT_CMF_4
	                                                                                                             FROM RWIPLOTSTS
	                                                                                                            WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                                                              AND LOT_TYPE = 'W'
	                                                                                                              AND MAT_ID LIKE 'GC%'
	                                                                                                              AND MAT_ID LIKE P_MAT_ID || '%'
	                                                                                                              AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                                                                     WHERE A.SLOT_ID = B.LOT_ID
	                                                                       AND A.FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                       AND A.SLOT_SEQ LIKE 'A%'
	                                                                       AND A.CUST_CODE = 'GC'
	                                                                       AND A.SHIP_FLAG = 'Y'
	                                                                       AND A.MAT_ID LIKE 'GC%') B
	                                               WHERE A.LOT_ID = B.LOT_ID
	                                                 AND A.FACTORY = 'HMKT1' 
	                                                 AND A.OPER = 'T1300'
	                                                 AND A.FROM_TO_FLAG = 'F'
	                                                 AND A.HIST_DEL_FLAG = ' '
	                                                 AND A.MAT_ID LIKE 'GC%') B
	                         WHERE A.LOT_ID = B.MERGE_LOT
	                           AND A.OPER = B.MERGE_OPER
	                           AND A.LOT_ID = B.LOT_ID
	                           AND A.LOT_CMF_4 = B.LOT_CMF_4
	                           AND A.TRAN_CODE = 'MERGE'
	                           AND A.HIST_DEL_FLAG = ' ')
	                 GROUP BY GCT_TEST_LOT_ID
	                UNION ALL
	                -- (+) 출하이력이 없는 LOT들이 다른 LOT에 MERGE되어 출하된 수량을 더하기 한다.
	                SELECT GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID
	                     , SUM(SHIP_QTY) AS SHIP_QTY
	                     , MAX(ACT_SHIP_TIME) AS ACT_SHIP_TIME
	                  FROM (SELECT A.LOT_CMF_4 AS GCT_TEST_LOT_ID
	                             , CASE WHEN A.OLD_QTY_1 > A.QTY_1 THEN A.OLD_QTY_1 - A.QTY_1
	                                                               ELSE 0
	                               END AS SHIP_QTY
	                             , A.TRAN_TIME AS ACT_SHIP_TIME
	                          FROM RWIPLOTHIS A, (SELECT A.FROM_TO_LOT_ID AS MERGE_LOT
	                                                   , A.OPER AS MERGE_OPER
	                                                   , B.LOT_ID AS LOT_ID
	                                                   , B.LOT_CMF_4 AS LOT_CMF_4
	                                                FROM RWIPLOTMRG A, (SELECT B.LOT_CMF_4 AS LOT_CMF_4
	                                                                         , A.SLOT_ID AS LOT_ID
	                                                                         , A.SBOX_QTY AS SHIP_OUT_QTY
	                                                                      FROM MESMGR.CWIPLBOXHIS@RPTTOMES A, (SELECT LOT_ID AS LOT_ID
	                                                                                                                , LOT_CMF_4 AS LOT_CMF_4
	                                                                                                             FROM RWIPLOTSTS
	                                                                                                            WHERE FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                                                              AND LOT_TYPE = 'W' 
	                                                                                                              AND MAT_ID LIKE 'GC%' 
	                                                                                                              AND MAT_ID LIKE P_MAT_ID || '%'
	                                                                                                              AND LOT_CMF_5 LIKE P_LOT_TYPE || '%') B
	                                                                     WHERE A.SLOT_ID = B.LOT_ID
	                                                                       AND A.FACTORY IN ('HMKT1', 'CUSTOMER')
	                                                                       AND A.SLOT_SEQ LIKE 'A%'
	                                                                       AND A.CUST_CODE = 'GC'
	                                                                       AND A.SHIP_FLAG = 'Y'
	                                                                       AND A.MAT_ID LIKE 'GC%') B
	                                               WHERE A.LOT_ID = B.LOT_ID
	                                                 AND A.FACTORY = 'HMKT1' 
	                                                 AND A.OPER = 'T1300'
	                                                 AND A.FROM_TO_FLAG = 'T'
	                                                 AND A.HIST_DEL_FLAG = ' '
	                                                 AND A.MAT_ID LIKE 'GC%') B
	                         WHERE A.LOT_ID = B.MERGE_LOT
	                           AND A.OPER = B.MERGE_OPER
	                           AND A.FROM_TO_LOT_ID = B.LOT_ID
	                           AND A.LOT_CMF_4 <> B.LOT_CMF_4
	                           AND A.TRAN_CODE = 'MERGE'
	                           AND A.HIST_DEL_FLAG = ' ')
	                 GROUP BY GCT_TEST_LOT_ID)
	         GROUP BY GCT_TEST_LOT_ID) J 
	     , (SELECT A.GCT_TEST_LOT_ID AS GCT_TEST_LOT_ID 
	             , A.PR_NO AS PR_NO 
	             , B.GCT_PROJECT_NO AS GCT_PROJECT_NO 
	             , C.WFR_LOT_ID AS WFR_LOT_ID 
	          FROM (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID 
	                     , A.ATTR_VALUE AS PR_NO 
	                  FROM MESMGR.MATRNAMSTS@RPTTOMES A, (SELECT DISTINCT LOT_CMF_4
	                                                        FROM (SELECT LOT_CMF_4 AS LOT_CMF_4
	                                                                FROM RWIPLOTSTS 
	                                                               WHERE FACTORY = 'HMKT1' 
	                                                                 AND MAT_ID LIKE 'GC%' 
	                                                                 AND LOT_TYPE = 'W' 
	                                                                 AND LOT_DEL_FLAG = ' ' 
	                                                                 AND QTY_1 > 0 
	                                                                 AND MAT_ID LIKE P_MAT_ID || '%' 
	                                                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	                                                              UNION ALL
	                                                              SELECT A.LOT_CMF_4 AS LOT_CMF_4
	                                                                FROM RWIPLOTSTS A, (SELECT A.FROM_TO_LOT_ID AS LOT_ID
	                                                                                      FROM RWIPLOTMRG A, (SELECT LOT_ID AS LOT_ID
	                                                                                                               , LOT_CMF_4 AS LOT_CMF_4
	                                                                                                            FROM RWIPLOTSTS
	                                                                                                           WHERE FACTORY = 'HMKT1'
	                                                                                                             AND MAT_ID LIKE 'GC%' 
	                                                                                                             AND LOT_TYPE = 'W' 
	                                                                                                             AND LOT_DEL_FLAG = ' '
	                                                                                                             AND QTY_1 > 0 
	                                                                                                             AND MAT_ID LIKE P_MAT_ID || '%' 
	                                                                                                             AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' ) B
	                                                                                     WHERE A.LOT_ID = B.LOT_ID
	                                                                                       AND A.FACTORY = 'HMKT1' 
	                                                                                       AND A.OPER = 'T1300'
	                                                                                       AND A.MAT_ID LIKE 'GC%' 
	                                                                                       AND A.HIST_DEL_FLAG = ' ') B
	                                                              WHERE A.LOT_ID = B.LOT_ID)) B 
	                 WHERE A.ATTR_KEY = B.LOT_CMF_4 
	                   AND A.FACTORY = 'HMKT1' 
	                   AND A.ATTR_TYPE = 'LOT_GCT' 
	                   AND A.ATTR_NAME = 'PRNO') A 
	             , (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID 
	                     , A.ATTR_VALUE AS GCT_PROJECT_NO 
	                  FROM MESMGR.MATRNAMSTS@RPTTOMES A, (SELECT DISTINCT LOT_CMF_4
	                                                        FROM (SELECT LOT_CMF_4 AS LOT_CMF_4
	                                                                FROM RWIPLOTSTS 
	                                                               WHERE FACTORY = 'HMKT1' 
	                                                                 AND MAT_ID LIKE 'GC%' 
	                                                                 AND LOT_TYPE = 'W' 
	                                                                 AND LOT_DEL_FLAG = ' ' 
	                                                                 AND QTY_1 > 0 
	                                                                 AND MAT_ID LIKE P_MAT_ID || '%' 
	                                                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	                                                              UNION ALL
	                                                              SELECT A.LOT_CMF_4 AS LOT_CMF_4
	                                                                FROM RWIPLOTSTS A, (SELECT A.FROM_TO_LOT_ID AS LOT_ID
	                                                                                      FROM RWIPLOTMRG A, (SELECT LOT_ID AS LOT_ID
	                                                                                                               , LOT_CMF_4 AS LOT_CMF_4
	                                                                                                            FROM RWIPLOTSTS
	                                                                                                           WHERE FACTORY = 'HMKT1'
	                                                                                                             AND MAT_ID LIKE 'GC%' 
	                                                                                                             AND LOT_TYPE = 'W' 
	                                                                                                             AND LOT_DEL_FLAG = ' '
	                                                                                                             AND QTY_1 > 0 
	                                                                                                             AND MAT_ID LIKE P_MAT_ID || '%' 
	                                                                                                             AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' ) B
	                                                                                     WHERE A.LOT_ID = B.LOT_ID
	                                                                                       AND A.FACTORY = 'HMKT1' 
	                                                                                       AND A.OPER = 'T1300'
	                                                                                       AND A.MAT_ID LIKE 'GC%' 
	                                                                                       AND A.HIST_DEL_FLAG = ' ') B
	                                                              WHERE A.LOT_ID = B.LOT_ID)) B 
	                 WHERE A.ATTR_KEY = B.LOT_CMF_4 
	                   AND A.FACTORY = 'HMKT1' 
	                   AND A.ATTR_TYPE = 'LOT_GCT' 
	                   AND A.ATTR_NAME = 'PROJECT_NO') B 
	             , (SELECT B.LOT_CMF_4 AS GCT_TEST_LOT_ID 
	                     , A.ATTR_VALUE AS WFR_LOT_ID 
	                  FROM MESMGR.MATRNAMSTS@RPTTOMES A, (SELECT DISTINCT LOT_CMF_4
	                                                        FROM (SELECT LOT_CMF_4 AS LOT_CMF_4
	                                                                FROM RWIPLOTSTS 
	                                                               WHERE FACTORY = 'HMKT1' 
	                                                                 AND MAT_ID LIKE 'GC%' 
	                                                                 AND LOT_TYPE = 'W' 
	                                                                 AND LOT_DEL_FLAG = ' ' 
	                                                                 AND QTY_1 > 0 
	                                                                 AND MAT_ID LIKE P_MAT_ID || '%' 
	                                                                 AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' 
	                                                              UNION ALL
	                                                              SELECT A.LOT_CMF_4 AS LOT_CMF_4
	                                                                FROM RWIPLOTSTS A, (SELECT A.FROM_TO_LOT_ID AS LOT_ID
	                                                                                      FROM RWIPLOTMRG A, (SELECT LOT_ID AS LOT_ID
	                                                                                                               , LOT_CMF_4 AS LOT_CMF_4
	                                                                                                            FROM RWIPLOTSTS
	                                                                                                           WHERE FACTORY = 'HMKT1'
	                                                                                                             AND MAT_ID LIKE 'GC%' 
	                                                                                                             AND LOT_TYPE = 'W' 
	                                                                                                             AND LOT_DEL_FLAG = ' '
	                                                                                                             AND QTY_1 > 0 
	                                                                                                             AND MAT_ID LIKE P_MAT_ID || '%' 
	                                                                                                             AND LOT_CMF_5 LIKE P_LOT_TYPE || '%' ) B
	                                                                                     WHERE A.LOT_ID = B.LOT_ID
	                                                                                       AND A.FACTORY = 'HMKT1' 
	                                                                                       AND A.OPER = 'T1300'
	                                                                                       AND A.MAT_ID LIKE 'GC%' 
	                                                                                       AND A.HIST_DEL_FLAG = ' ') B
	                                                              WHERE A.LOT_ID = B.LOT_ID)) B 
	                 WHERE A.ATTR_KEY = B.LOT_CMF_4 
	                   AND A.FACTORY = 'HMKT1' 
	                   AND A.ATTR_TYPE = 'LOT_GCT' 
	                   AND A.ATTR_NAME = 'FAB_LOT_ID') C 
	         WHERE A.GCT_TEST_LOT_ID = B.GCT_TEST_LOT_ID(+) 
	           AND A.GCT_TEST_LOT_ID = C.GCT_TEST_LOT_ID(+)) K 
	 WHERE A.GCT_TEST_LOT_ID = B.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = C.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = D.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = E.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = F.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = G.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = H.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = I.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = J.GCT_TEST_LOT_ID(+) 
	   AND A.GCT_TEST_LOT_ID = K.GCT_TEST_LOT_ID(+) 
	 ORDER BY A.GCT_TEST_LOT_ID ASC;
	
	COMMIT;

	-- 2012-06-22-정비재 : LOG기록을 위하여 PROC 종료시간을 가져온다.
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

/************************************************************************************************************************************************************************************/
/************************************************************************************************************************************************************************************/

END PACKAGE_PRD010103;
/