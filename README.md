# TCC-Site-Thaibev
โจทย์สำหรับทดสอบ ทำ Backend Api : C# หรือ Golang  Version ใหม่สุดเท่าที่ทำได้ ทำ Frontend :  Vue หรือ Angular

มีทั้งหมด 10 ข้อ ดังนี้
1. ออกแบบ UI และ DB สำหรับกรอกและบันทึกข้อมูล ชื่อ-สกุล วันเกิด และข้อมูลที่อยู่ โดยแสดงอายุจากปีปัจจุบัน - ปีเกิด
...
10.

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

จากนั้นเริ่มในส่วนของ frontend ถ้าปกติอาจจะออกแบบเองผ่าน figma แต่นี่มีแบบให้แล้วเลยเริ่มจากไปที่ path ของโปรเจค
จากนั้นสร้างโปรเจ็ค angular หรือใช้คำสั่ง ng new {ชื่อโปรเจ็คเว็บ ในที่นี้ผมใช้ tcc-ui}
แล้วก็ไปที่โฟลเดอร์ tcc-ui แล้วทำการเพิ่ม service อาจใช้คำสั่ง ng generate service services/person เพื่อทำการดึง API
จากนั้นแก้ใน program.cs ให้ AddCors เพื่อ AllowAngular

หลังจากนั้นก็ทำการต่อ API GetAll และ Add เพื่อนำมาใช้กับ CRUD และแสดงข้อมูลในตาราง
สร้าง Model Person สำหรับเป็นโครงในการ export ไปใช้ส่งข้อมูลต่อในส่วนของ Component

Component ประกอบไปด้วย html, css และ js(ในที่นี้คือ typescript)
ทีนี้เราก็มาดู UX flow ที่ออกแบบไว้ตอนแรกว่ามี action หรือ function อะไรใช้บ้าง
จากนั้นก็ใช้ *ngFor ในการวนค่า persons$ โดยใช้ async และ index
แล้วเราก็ทำการสร้าง modal สำหรับ view และ add ข้อมูล

จากนั้นผมก็ทำการเปิด flag สำหรับ trigger action ต่างๆ  ได้แก่
modal = 1;
page = 1;
showModal = false;
modalMode: 'view' | 'add' = 'view';

แล้้วทำการกำหนด object
 persons$ = new BehaviorSubject<Person[]>([]);
 selectedPerson: Person | null = null;
 newPerson: Person = {
   firstName: '',
   lastName: '',
   birthDate: '',
   address: ''
 };

เพื่อให้สอดคล้องกับ service ที่เขียนไว้
getAll(page: number) {
  return this.http.get<Person[]>(
    `${this.apiUrl}?page=${page}`
  );
}

add(person: Person) {
  return this.http.post<Person>(
    `${this.apiUrl}`,
    person
  );
}


หลังจากประกอบ API และหน้าเว็บสำเร็จก็ทำการเทสอีกรอบ
ด้วยการเปิดใช้งาน API ที่ .\TCC-Site-Thaibev\No1\TCC No1 Test\ ด้วยคำสั่ง dotnet run

และเปิดหน้าเว็บที่ .\TCC-Site-Thaibev\No1\TCC No1 Test\tcc-ui\ ด้วยคำสั่ง ng nerve



ผมเริ่มวิเคราะห์ข้อ No2
Functional
- User สามารถสมัครสมาชิกใหม่
- มีการ validation login ด้วย jwt
