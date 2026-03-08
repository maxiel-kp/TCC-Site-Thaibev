# TCC-Site-Thaibev
เมื่อทำเสร็จแล้วรบกวนอัพลง Git Hub และส่ง link คำตอบกลับมาให้ด้วยนะคะ  ทำ Backend Api : C# หรือ Golang  Version ใหม่สุดเท่าที่ทำได้ ทำ Frontend :  Vue หรือ Angular

มีทั้งหมด 10 ข้อ ดังนี้
1. ออกแบบ UI และ DB สำหรับกรอกและบันทึกข้อมูล ชื่อ-สกุล วันเกิด และข้อมูลที่อยู่ โดยแสดงอายุจากปีปัจจุบัน - ปีเกิด 
2.
3.
4.
5.
6.
7.
8.
9.

ผมเริ่มวิเคราะห์ข้อ No1
Functional
- User สามารถเพิ่มข้อมูล Person
- ระบบต้องคำนวณอายุจาก ปีปัจจุบัน - ปีเกิด
- User ไม่สามารถแก้ไขข้อมูลได้ (Read-only)
- ข้อมูลใหม่ต้องแสดงท้ายตาราง
- ถ้ากดปุ่มยกเลิกจะเป็นการยกเลิกการเพิ่มข้อมูล

Non-Functional
- UI ตามโจทย์กำหนด
- Response time เร็ว
- Data consistency

หลังจากนั้นออกแบบ UX flow และ ER Diagram ผ่าน Diagram.net
แล้วจากนั้นออกแบบ API จากนั้นจัดทำ API Specification Doc

ที่นี่ก็มาเขียน API โดยเปิด visual studio 2022 แล้ว New web api (.Net)

จากนั้นวางโครง Domain-driven desgin โดยยังคำนึงถึง Clean architecture
จึงออกมาเป็น
- Controllers
- Data (DALs)
- DTOs (request/response)
- Entities (Models)
- Repositories
- Services

ต่อมาผมนำโครงสร้างตาราง Person มาแปลงเป็น Entity ที่ class Person
แล้วทำการ code first โดยเริ่มจากต่อ Connection string ที่ appsetting และ AppDbContext ในโฟลเดอร์ Data
จากนั้น inherit DbContext เพิ่มเพิ่ม DbSet ให้ class person

ต่อมาเปิด console แล้วใช้คำสั่ง Add-Migration InitialCreate เพื่อสร้าง snapshot 
จากนั้นใช้คำสั่ง Update-Database เพื่อสร้างตาราง Persons ใน SqlServer ที่ต่อไว้


หลังจากได้ Persons ใน SqlServer แล้วเราจะเริ่มทำการสร้าง CRUD API
เริ่มจาก add controller เพื่อสร้าง PersonController
จากนั้นทำการสร้าง DTOs สำหรับ request และ response ตาม API Spec ที่ออกแบบไว้
หลังจากนั้นสร้าง constructor ของ PersonController เพื่อใส่ field ในส่วน interface ของ personService

ในส่วนของ PersonService จะทำการสร้าง constructor เพื่อใส่ field ในส่วน interface ของ IPersonRepository
แล้วเราต้อง DI(Dependency Injection) เพิ่ม resolve service ด้วยการ AddScoped ตัว interface ที่ใช้ในโปรเจคนี้

ในส่วนของ PersonRepository ผมจะทำการเพิ่ม Method เบื้องต้นตาม API spec ออกแบบไว้ ได้แก่
GetAll, GetById และ Add ซึ่ง API ตรงนี้จะมีการดึงข้อมูลเป็นจำนวนมากเลยทำ Async เผื่อไว้เลย

หลังจากนั้นผมก็เทส API ที่สร้างทั้งใส่ข้อมูลถูกและผิดตาม test case scenario ที่เกิดขึ้นเพื่อเช็คความถูกต้อง
จากนั้นก็ทำการเขียน Unit test เพื่อความง่ายในการเทสต่อไป
