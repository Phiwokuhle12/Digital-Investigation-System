function Student(props) {
    return (
        <div>
            <h2>{props.name}</h2>
            <p>Course: {props.course}</p>
        </div>
    );

}
export default Student;