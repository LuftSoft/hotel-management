import Stars from "../Stars";

export default function ServiceRating({ name = "Sạch sẽ", numOfStar }) {
	return (
		<div className="d-flex">
			<div className="me-2" style={{ flexBasis: "40%" }}>
				{name}
			</div>
			<div className="flex-grow-1">
				{/* 5 */}
				<div className="d-flex align-items-center">
					<div className="d-flex align-items-center justify-content-between">
						<Stars numberOfStar={numOfStar} />
					</div>
				</div>
			</div>
		</div>
	);
}
