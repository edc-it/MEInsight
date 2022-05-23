use [MEInsight]

SET IDENTITY_INSERT [dbo].[RefOrganizationType] ON 
GO
INSERT [dbo].[RefOrganizationType] ([RefOrganizationTypeId], [OrganizationType], [OrganizationTypeCode]) VALUES (1, N'Organization Unit', N'OU')
GO
INSERT [dbo].[RefOrganizationType] ([RefOrganizationTypeId], [OrganizationType], [OrganizationTypeCode]) VALUES (2, N'School', N'School')
GO
SET IDENTITY_INSERT [dbo].[RefOrganizationType] OFF
GO
INSERT [dbo].[Organization] ([OrganizationId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [RegistrationDate], [OrganizationCode], [OrganizationName], [RefOrganizationTypeId], [ParentOrganizationId], [Contact], [Phone], [RefLocationId], [Address], [Latitude], [Longitude], [IsOrganizationUnit], [IsTenant]) VALUES (N'9c1f1204-62d7-46a2-b5b0-a94affa54b22', NULL, CAST(N'2020-03-17T22:41:47.6462286+00:00' AS DateTimeOffset), NULL, NULL, NULL, NULL, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'EDC', N'Management Organization', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1)
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [OrganizationId], [FirstName], [LastName], [IsDeleted], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate]) VALUES (N'b17badca-b6ce-42e8-6fad-08d7cac46093', N'admin@meinsight.edc.org', N'ADMIN@MEINSIGHT.EDC.ORG', N'admin@meinsight.edc.org', N'ADMIN@MEINSIGHT.EDC.ORG', 0, N'AQAAAAEAACcQAAAAENXxphy9iYawB/WX+CkhWtWm7oyVFFFc0userRINIFFlKePk2Zi2gE1IB2cH543QOg==', N'YAQTU7T3TW2ZNPA2AKFS52JIL7L6QDK6', N'8b2a5f3b-8ff9-4207-8a61-28dcb2302c22', NULL, 0, 0, NULL, 1, 0, N'9c1f1204-62d7-46a2-b5b0-a94affa54b22', N'Admin', N'', 0, N'system', CAST(N'2020-03-17T18:41:47.8396745' AS DateTime2), NULL, NULL, NULL, NULL)

GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Description]) VALUES (N'42649b3c-4bb9-4bcc-e741-08d7cac4607c', N'Administrator', N'ADMINISTRATOR', N'921d3299-4b40-42dd-a2cc-d91668867832', N'DBMS Administrator Role')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Description]) VALUES (N'416ec688-018f-424d-e742-08d7cac4607c', N'Read', N'READ', N'6fd73baf-a692-4e9f-83ed-012faf830211', N'Read only Role')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Description]) VALUES (N'556d6c7f-baed-4d15-e743-08d7cac4607c', N'Create', N'CREATE', N'49f333a9-2333-43ab-a0b3-b0d2c9113aad', N'Create only Role')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Description]) VALUES (N'53ed8f85-f93b-47e2-e744-08d7cac4607c', N'Edit', N'EDIT', N'd600c0fc-6736-4419-a30e-bc09857c1803', N'Create and Edit Role')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Description]) VALUES (N'b5efcce1-77e1-4a81-e745-08d7cac4607c', N'Delete', N'DELETE', N'1ed4ea6d-9de9-4ca2-9c81-2b0e945bc624', N'Create, Edit, and Delete Role')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Description]) VALUES (N'431768eb-cb9b-49cb-e746-08d7cac4607c', N'MELOfficer', N'MELOFFICER', N'1e545ab0-7986-47a4-a9ed-ca66686827d8', N'Monitoring, Evaluation and Learning Officer Role')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Description]) VALUES (N'71d97eda-c86c-4f02-e747-08d7cac4607c', N'MEL', N'MEL', N'8424d632-a07c-434a-ae3f-04261327f28c', N'Monitoring, Evaluation and Learning Role')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b17badca-b6ce-42e8-6fad-08d7cac46093', N'42649b3c-4bb9-4bcc-e741-08d7cac4607c')
GO

GO
SET IDENTITY_INSERT [dbo].[RefParticipantType] ON 
GO
INSERT [dbo].[RefParticipantType] ([RefParticipantTypeId], [ParticipantTypeCode], [ParticipantType]) VALUES (1, N'ST', N'Student')
GO
INSERT [dbo].[RefParticipantType] ([RefParticipantTypeId], [ParticipantTypeCode], [ParticipantType]) VALUES (2, N'TE', N'Teacher')
GO
INSERT [dbo].[RefParticipantType] ([RefParticipantTypeId], [ParticipantTypeCode], [ParticipantType]) VALUES (3, N'EA', N'Education Administrator')
GO
SET IDENTITY_INSERT [dbo].[RefParticipantType] OFF
GO
INSERT [dbo].[RefSex] ([RefSexId], [Sex], [SexId]) VALUES (1, N'Male', N'M')
GO
INSERT [dbo].[RefSex] ([RefSexId], [Sex], [SexId]) VALUES (2, N'Female', N'F')
GO
SET IDENTITY_INSERT [dbo].[RefEnrollmentStatus] ON 
GO
INSERT [dbo].[RefEnrollmentStatus] ([RefEnrollmentStatusId], [EnrollmentStatusCode], [EnrollmentStatus]) VALUES (1, N'E', N'Enrolled')
GO
INSERT [dbo].[RefEnrollmentStatus] ([RefEnrollmentStatusId], [EnrollmentStatusCode], [EnrollmentStatus]) VALUES (2, N'C', N'Completed')
GO
INSERT [dbo].[RefEnrollmentStatus] ([RefEnrollmentStatusId], [EnrollmentStatusCode], [EnrollmentStatus]) VALUES (3, N'D', N'Dropped out')
GO
SET IDENTITY_INSERT [dbo].[RefEnrollmentStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[RefAttendanceUnit] ON 
GO
INSERT [dbo].[RefAttendanceUnit] ([RefAttendanceUnitId], [AttendanceUnitCode], [AttendanceUnit], [AttendanceUnitId]) VALUES (1, N'Hour', N'Hours', N'h')
GO
INSERT [dbo].[RefAttendanceUnit] ([RefAttendanceUnitId], [AttendanceUnitCode], [AttendanceUnit], [AttendanceUnitId]) VALUES (2, N'Day', N'Days', N'd')
GO
SET IDENTITY_INSERT [dbo].[RefAttendanceUnit] OFF
GO
SET IDENTITY_INSERT [dbo].[RefEvaluationStatus] ON 
GO
INSERT [dbo].[RefEvaluationStatus] ([RefEvaluationStatusId], [EvaluationStatusCode], [EvaluationStatus]) VALUES (1, N'E', N'Enrolled')
GO
INSERT [dbo].[RefEvaluationStatus] ([RefEvaluationStatusId], [EvaluationStatusCode], [EvaluationStatus]) VALUES (2, N'C', N'Completed')
GO
SET IDENTITY_INSERT [dbo].[RefEvaluationStatus] OFF
