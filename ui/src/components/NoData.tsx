const NoData = (props: { title: string }) => {
  return (
    <div
      className="d-flex flex-column justify-content-center align-items-center"
      style={{ height: "50vh" }}
    >
      <h2>No movies found by the provided title ({props.title})</h2>
      <i
        className="fas fa-frown"
        style={{ fontSize: "50px", color: "#ccc" }}
      ></i>
    </div>
  );
};

export default NoData;
