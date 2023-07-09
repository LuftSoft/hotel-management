import star from "../../assets/star.svg";

export default function CommentCard({ comment }) {
	return (
		<div className="d-flex flex-column">
			<div className="d-flex gap-4 p-3 border border-2 rounded">
				<div
					className="d-flex flex-column"
					style={{
						flexBasis: "25%",
					}}>
					<div className="d-flex flex-column">
						<div className="d-flex align-items-center">
							<div
								className="me-2 p-1 border border-3 rounded-circle"
								style={{
									width: 64,
									height: 64,
								}}>
								<img className="w-100 h-100 rounded" src="/img/user-avatar.png" alt="avatar" />
							</div>
							<div className="d-flex flex-column flex-grow-1 fw-bold">{comment.userName}</div>
						</div>
					</div>
				</div>
				<div className="d-flex flex-column gap-2 flex-grow-1">
					<div className="d-flex justify-content-between">
						<div className="badge rounded-pill bg-info d-flex align-items-center px-2 py-1">
							<div className="d-flex align-items-center">
								<div className="d-flex me-1">
									<img src={star} alt="star" />
								</div>
								<span>{comment.rating}</span>
								<span className="mx-1">/</span>
								<span>5</span>
							</div>
						</div>
						<div className="text-secondary">{new Date(comment.lastChange).toDateString()}</div>
					</div>
					<div className="d-flex flex-column">
						<div>{comment.content}</div>
					</div>
					<div className="d-flex align-items-center">
						<div
							style={{
								width: 33,
								height: 33,
								cursor: "pointer",
							}}>
							<i className="fa-regular fa-thumbs-up fs-4"></i>
						</div>
						<div className="d-flex user-select-none">
							<h4 className="fs-6">Đánh giá này hữu ích không?</h4>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}
