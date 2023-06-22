import nulllIcon from "../../assets/null-icon.svg";

export default function EmptyCard({ title, content }) {
	return (
		<div className="border rounded bg-white p-3 d-flex">
			<img src={nulllIcon} alt="null-icon" />
			<div className="d-flex flex-column mx-3">
				<h3 className="fs-6 fw-bold my-2">{title}</h3>
				<div>{content}</div>
			</div>
		</div>
	);
}
