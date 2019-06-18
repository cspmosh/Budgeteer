#Budgeteer Database Creation SQL

CREATE DATABASE IF NOT EXISTS Budgeteer;

USE Budgeteer

#Create Tables

#Create Users Table
CREATE TABLE IF NOT EXISTS Users
	(User_ID VARCHAR(30) NOT NULL PRIMARY KEY, Password VARCHAR(40) NOT NULL, Salt VARCHAR(12) NOT NULL, First_Name VARCHAR(50) NOT NULL, Last_Name VARCHAR(50) NOT NULL, Email_Address VARCHAR(50) NOT NULL UNIQUE, Active TINYINT(1) NOT NULL DEFAULT 0, Last_Updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP);

#Create User Registrations Table
CREATE TABLE IF NOT EXISTS User_Registrations
	(Registration_Key CHARACTER(36) NOT NULL PRIMARY KEY, User_ID VARCHAR(30) NOT NULL UNIQUE, INDEX user_index (User_id), FOREIGN KEY (User_ID) REFERENCES Users(User_ID) ON UPDATE CASCADE ON DELETE CASCADE, Email_Address VARCHAR(50) NOT NULL UNIQUE, Last_Updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP);
	
#Create Security Questions Table
CREATE TABLE IF NOT EXISTS Security_Questions
	(Security_Question_ID BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY, Question VARCHAR(255) NOT NULL UNIQUE);

# Insert security questions
INSERT INTO security_questions(question) VALUES ('What was your childhood nickname?');
INSERT INTO security_questions(question) VALUES ('In what city did you meet your spouse/significant other?');
INSERT INTO security_questions(question) VALUES ('What is the name of your favorite childhood friend?');
INSERT INTO security_questions(question) VALUES ('What is your oldest sibling\'s birthday month and year? (e.g., January 1900)');
INSERT INTO security_questions(question) VALUES ('What is the middle name of your youngest child?');
INSERT INTO security_questions(question) VALUES ('What is your oldest sibling\'s middle name?');
INSERT INTO security_questions(question) VALUES ('What school did you attend for sixth grade?');
INSERT INTO security_questions(question) VALUES ('What is your oldest cousin\'s first and last name?');
INSERT INTO security_questions(question) VALUES ('What was the name of your first stuffed animal?');
INSERT INTO security_questions(question) VALUES ('In what city or town did your mother and father meet?');
INSERT INTO security_questions(question) VALUES ('Where were you when you had your first kiss?');
INSERT INTO security_questions(question) VALUES ('What is the first name of the boy or girl that you first kissed?');
INSERT INTO security_questions(question) VALUES ('What was the last name of your third grade teacher?');
INSERT INTO security_questions(question) VALUES ('In what city does your nearest sibling live?');
INSERT INTO security_questions(question) VALUES ('What is your youngest brother\'s birthday month and year? (e.g., January 1900)');
INSERT INTO security_questions(question) VALUES ('What is your maternal grandmother\'s maiden name?');
INSERT INTO security_questions(question) VALUES ('In what city or town was your first job?');
INSERT INTO security_questions(question) VALUES ('What is the name of the place your wedding reception was held?');
INSERT INTO security_questions(question) VALUES ('What is the name of a college you applied to but didn\'t attend?');
INSERT INTO security_questions(question) VALUES ('What is the name of the company of your first job?');

#Create User Security Questions Table
CREATE TABLE IF NOT EXISTS User_Security_Questions
	(User_ID VARCHAR(30) NOT NULL, Security_Question_ID BIGINT NOT NULL, Answer VARCHAR(50) NOT NULL, FOREIGN KEY (User_ID) REFERENCES Users(User_ID) ON UPDATE CASCADE ON DELETE CASCADE, FOREIGN KEY (Security_Question_ID) REFERENCES Security_Questions(Security_Question_ID) ON UPDATE CASCADE ON DELETE RESTRICT, PRIMARY KEY(User_ID, Security_Question_ID));
	
#Create Application Settings
CREATE TABLE IF NOT EXISTS Application_Settings
	(Setting VARCHAR(30) NOT NULL, Value VARCHAR(30), User_ID VARCHAR(30) NOT NULL, INDEX user_index (User_id), FOREIGN KEY (User_ID) REFERENCES Users(User_ID) ON UPDATE CASCADE ON DELETE CASCADE, UNIQUE uc_settingUserID(Setting, User_ID), Last_Updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP);

#Create Accounts Table
CREATE TABLE IF NOT EXISTS Accounts 
	(Account_ID BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY, Number VARCHAR(4), Name VARCHAR(50) NOT NULL, Type VARCHAR(20), Balance DECIMAL(15,2) NOT NULL DEFAULT 0.00, Active TINYINT(1) NOT NULL DEFAULT 1, User_ID VARCHAR(30) NOT NULL, INDEX user_index (User_id), FOREIGN KEY (User_ID) REFERENCES Users(User_ID) ON UPDATE CASCADE ON DELETE CASCADE, Last_Updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, UNIQUE uc_nameUserID(Name, Number, User_ID));

#Create Categories Table
CREATE TABLE IF NOT EXISTS Categories
	(Category_ID BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY, Description VARCHAR(50) NOT NULL, Active TINYINT(1) NOT NULL DEFAULT 1, User_ID VARCHAR(30) NOT NULL, INDEX user_index (User_id), FOREIGN KEY (User_ID) REFERENCES Users(User_ID) ON UPDATE CASCADE ON DELETE CASCADE, UNIQUE uc_descriptionUserID(Description, User_ID), Last_Updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP);

#Create Subcategories Table	
CREATE TABLE IF NOT EXISTS Subcategories
	(Subcategory_ID BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY, Category_ID BIGINT NOT NULL, INDEX category_index (Category_id), FOREIGN KEY (Category_ID) REFERENCES Categories(Category_ID) ON UPDATE CASCADE ON DELETE RESTRICT, Description VARCHAR(50) NOT NULL, Type VARCHAR(10) NOT NULL, Sinking_Fund TINYINT(1) NOT NULL DEFAULT 0, Balance DECIMAL(15,2) NOT NULL DEFAULT 0.00, Notes VARCHAR(255), Active TINYINT(1) NOT NULL DEFAULT 1, User_ID VARCHAR(30) NOT NULL, INDEX user_index (User_id), FOREIGN KEY (User_ID) REFERENCES Users(User_ID) ON UPDATE CASCADE ON DELETE CASCADE, UNIQUE uc_descriptionCategoryIDUserID(Description, Category_ID, User_ID), Last_Updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP);

#Create Budgets Table	
CREATE TABLE IF NOT EXISTS Budgets
	(Budget_ID BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY, Start_Date DATE NOT NULL, Subcategory_ID BIGINT NOT NULL, INDEX subcategory_index (Subcategory_ID), FOREIGN KEY (Subcategory_ID) REFERENCES Subcategories(Subcategory_ID) ON UPDATE CASCADE ON DELETE RESTRICT, Amount DECIMAL(9,2) NOT NULL, User_ID VARCHAR(30) NOT NULL, INDEX user_index (User_id), FOREIGN KEY (User_ID) REFERENCES Users(User_ID) ON UPDATE CASCADE ON DELETE CASCADE, UNIQUE uc_subcatStartDateUserID (Subcategory_ID, Start_Date, User_ID), Last_Updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP);

#Create Transactions Table
CREATE TABLE IF NOT EXISTS Transactions
	(Transaction_ID BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY, Date DATE NOT NULL, Check_Number INTEGER, Description VARCHAR(255) NOT NULL, Amount DECIMAL(9,2) NOT NULL DEFAULT 0.00, Tax_Amount DECIMAL(9,2) NOT NULL DEFAULT 0.00, Subcategory_ID BIGINT, INDEX Subcategory_Index (Subcategory_ID), FOREIGN KEY (Subcategory_ID) REFERENCES Subcategories(Subcategory_ID) ON UPDATE CASCADE ON DELETE RESTRICT, Account_ID BIGINT, INDEX Account_Index (Account_ID), FOREIGN KEY (Account_ID) REFERENCES Accounts(Account_ID) ON UPDATE CASCADE ON DELETE RESTRICT, User_ID VARCHAR(30) NOT NULL, INDEX user_index (User_id), FOREIGN KEY (User_ID) REFERENCES Users(User_ID) ON UPDATE CASCADE ON DELETE CASCADE, Last_Updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP);

#Create Budget Vs. Utilized Dollars View
CREATE View vw_budget_vs_utilized AS
select b.start_date, b.subcategory_id, sum(amount) as budgeted, COALESCE(sum((select sum(t.amount) from transactions t where t.subcategory_id = b.subcategory_id and YEAR(t.date) = YEAR(b.start_date) and MONTH(t.date) = MONTH(b.start_date))), 0.00) as utilized from budgets b group by b.start_date, b.subcategory_id;