import Student from "./Student";
function Apps() {
    const studentName = "Phiwokuhle";
    const course = "Web Development";

    return (
        <div>
            <h1>Student Information</h1>
            <Student name={studentName} course={course} />
        </div>
    );
   
}
export default Apps;