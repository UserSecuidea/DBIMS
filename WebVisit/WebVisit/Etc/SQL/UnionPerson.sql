SELECT b.PersonTypeID, b.PersonID, COALESCE(a.EMPLOYEE_ID, b.sabun, 'N') as Sabun, COALESCE(a.USER_NAME, b.Name, 'N') as Name, 
                        COALESCE(a.Location, b.Location, 'N') as Location, b.CompanyID, COALESCE(b.PersonTypeID, 0) as PersonTypeID, 
                        b.OrgID as OrgID, COALESCE(a.DEPT_NAME, b.OrgNameKor, 'N') as OrgNameKor, COALESCE(b.OrgNameEng, 'N') as OrgNameEng, 
                        COALESCE(b.LevelCodeID, 3) as LevelCodeID, COALESCE(b.GradeID, 0) as GradeID, COALESCE(a.SAP_TITLE_NAME, b.GradeName) as GradeName, 
                        COALESCE(a.PASSWORD, b.Password, 'N') as Password, b.PWUpdateDate, b.BirthDate, b.Gender, COALESCE(a.MOBILE, b.Mobile) as Mobile, 
                        COALESCE(b.Tel, '') as Tel, COALESCE(a.Email, b.Email) as Email, b.ImageData, b.ImageDataHash, COALESCE(CONVERT(int, a.IS_DELETED), b.PersonStatusID, 0) as PersonStatusID, 
                        b.HomeAddr, b.HomeDetailAddr, b.HomeZipcode, COALESCE(b.IsForeigner, 0) as IsForeigner, b.Nationality, b.ImmStatusCodeID, 
                        b.ImmStartDate, b.ImmEndDate, b.IsAllowSMS, b.WorkTypeCodeID, b.CarAllowCnt, b.CarRegCnt, b.TermsPrivarcyFileData, 
                        b.TermsPrivarcyFileDataHash, b.CardIssueStatus, b.CardCreateCnt, CONVERT(CHAR(10),b.VisitorEduLastDate,23) as VisitorEduLastDate, 
                        CONVERT(CHAR(10), b.CleanEduLastDate,23) as CleanEduLastDate, CONVERT(CHAR(10),b.SafetyEduLastDate,23) as SafetyEduLastDate, 
                        CONVERT(CHAR(10),b.VisitLastDate,23) as VisitLastDate, CONVERT(CHAR(10),b.ValidDate,23) as ValidDate, b.UpdateIP, b.CardID, b.CardNo, b.IsRecTempCard, 
                        CONVERT(CHAR(10),b.TempCardRecDate,23) as TempCardRecDate, COALESCE(b.InsertSabun,'N') as InsertSabun, b.InsertName, b.InsertOrgID, 
                        b.InsertOrgNameKor, b.InsertOrgNameEng, CONVERT(CHAR(10), b.UpdateDate,23) as UpdateDate, CONVERT(CHAR(10),b.InsertDate,23) as InsertDate, 
                        COALESCE(b.IsDelete,'N') as IsDelete, b.PID 
                        FROM (SELECT * FROM PASSPORT.dbo.V_IMS_USER_INFO  WHERE USER_NAME IS NOT NULL) a FULL OUTER JOIN (SELECT * from Person WHERE IsDelete = 'N' ) b ON (a.EMPLOYEE_ID = b.sabun) 
                        WHERE  (b.PersonStatusID = '0' OR a.IS_DELETED = '0')  AND  (b.Name = '송은지' OR a.USER_NAME = '송은지')  ORDER BY a.USER_NAME 