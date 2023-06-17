import star from "../../assets/star.svg";
import halfStar from "../../assets/half-star.svg";
import CommentCard from "../CommentCard/CommentCard";
import PostComment from "../PostComment/PostComment";

export default function CommentSection() {
	return (
		<div className="d-flex flex-column bg-white rounded">
			<div className="d-flex flex-column gap-3 px-3 py-4">
				<h2 className="fs-5 fw-bolder">Đánh giá từ khách hàng</h2>
				<div className="d-flex flex-column">
					<h3 className="fs-6">Xếp hạng và điểm đánh giá chung</h3>
					<div className="text-secondary" style={{ fontSize: 14 }}>
						{"Từ "}
						<strong>{"361"}</strong>
						{" đánh giá của khách đã ở"}
					</div>
					<div className="d-flex align-items-center mt-2">
						<div className="d-flex flex-column">
							<div>
								<div
									className="d-flex flex-column align-items-center justify-content-center mb-2 p-1 border border-5 border-info rounded-circle"
									style={{
										width: 128,
										height: 128,
									}}>
									<div
										className="d-flex flex-column bg-info rounded-circle"
										style={{
											width: "104px",
											height: "104px",
										}}>
										<div className="d-inline text-center text-white" style={{ lineHeight: "104px", fontSize: "48px" }}>
											4.5
										</div>
									</div>
								</div>
								<div className="d-flex flex-column justify-content-center">
									<div className="fs-4 text-center fw-bold text-info">
										<div className="d-flex align-items-center justify-content-between">
											<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
											<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
											<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
											<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
											<img className="d-block" src={halfStar} alt="star" style={{ width: 18, height: 18 }} />
										</div>
									</div>
								</div>
							</div>
						</div>
						<div
							className="d-flex flex-column"
							style={{
								flexGrow: 2,
								paddingLeft: 80,
							}}>
							{/* 5 */}
							<div>
								<div className="row">
									<div className="col-3">Tuyệt vời</div>
									<div className="col-7 px-4">
										<div className="progress">
											<div
												className="progress-bar bg-info"
												role="progressbar"
												style={{ width: "75%" }}
												aria-label="rating"
												aria-valuenow="75"
												aria-valuemin="0"
												aria-valuemax="100"></div>
										</div>
									</div>
									<div className="col-2">63</div>
								</div>
								<div className="row">
									<div className="col-3">Rất tốt</div>
									<div className="col-7 px-4">
										<div className="progress">
											<div
												className="progress-bar bg-info"
												role="progressbar"
												style={{ width: "75%" }}
												aria-label="rating"
												aria-valuenow="75"
												aria-valuemin="0"
												aria-valuemax="100"></div>
										</div>
									</div>
									<div className="col-2">63</div>
								</div>
								<div className="row">
									<div className="col-3">Rất tốt</div>
									<div className="col-7 px-4">
										<div className="progress">
											<div
												className="progress-bar bg-info"
												role="progressbar"
												style={{ width: "75%" }}
												aria-label="rating"
												aria-valuenow="75"
												aria-valuemin="0"
												aria-valuemax="100"></div>
										</div>
									</div>
									<div className="col-2">63</div>
								</div>
								<div className="row">
									<div className="col-3">Rất tốt</div>
									<div className="col-7 px-4">
										<div className="progress">
											<div
												className="progress-bar bg-info"
												role="progressbar"
												style={{ width: "75%" }}
												aria-label="rating"
												aria-valuenow="75"
												aria-valuemin="0"
												aria-valuemax="100"></div>
										</div>
									</div>
									<div className="col-2">63</div>
								</div>
								<div className="row">
									<div className="col-3">Rất tốt</div>
									<div className="col-7 px-4">
										<div className="progress">
											<div
												className="progress-bar bg-info"
												role="progressbar"
												style={{ width: "75%" }}
												aria-label="rating"
												aria-valuenow="75"
												aria-valuemin="0"
												aria-valuemax="100"></div>
										</div>
									</div>
									<div className="col-2">63</div>
								</div>
							</div>
						</div>
						<div className="d-flex flex-column flex-grow-1 ps-5 pe-4">
							<div>
								{/* 5 */}
								<div className="d-flex">
									<div className="flex-grow-1">Sạch sẽ</div>
									<div className="flex-grow-1">
										{/* 5 */}
										<div className="d-flex align-items-center">
											<div className="d-flex align-items-center justify-content-between">
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={halfStar} alt="star" style={{ width: 18, height: 18 }} />
											</div>
										</div>
									</div>
								</div>
								<div className="d-flex">
									<div className="flex-grow-1">Sạch sẽ</div>
									<div className="flex-grow-1">
										{/* 5 */}
										<div className="d-flex align-items-center">
											<div className="d-flex align-items-center justify-content-between">
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={halfStar} alt="star" style={{ width: 18, height: 18 }} />
											</div>
										</div>
									</div>
								</div>
								<div className="d-flex">
									<div className="flex-grow-1">Sạch sẽ</div>
									<div className="flex-grow-1">
										{/* 5 */}
										<div className="d-flex align-items-center">
											<div className="d-flex align-items-center justify-content-between">
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={halfStar} alt="star" style={{ width: 18, height: 18 }} />
											</div>
										</div>
									</div>
								</div>
								<div className="d-flex">
									<div className="flex-grow-1">Sạch sẽ</div>
									<div className="flex-grow-1">
										{/* 5 */}
										<div className="d-flex align-items-center">
											<div className="d-flex align-items-center justify-content-between">
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={halfStar} alt="star" style={{ width: 18, height: 18 }} />
											</div>
										</div>
									</div>
								</div>
								<div className="d-flex">
									<div className="flex-grow-1">Sạch sẽ</div>
									<div className="flex-grow-1">
										{/* 5 */}
										<div className="d-flex align-items-center">
											<div className="d-flex align-items-center justify-content-between">
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={star} alt="star" style={{ width: 18, height: 18 }} />
												<img className="d-block" src={halfStar} alt="star" style={{ width: 18, height: 18 }} />
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
				{/* list comment */}
				<div className="d-flex flex-column">
					{/* to comment */}
					{true && <PostComment />}
					{/* divider */}
					<div className="my-2"></div>
					{/* list comment */}
					<div className="d-flex flex-column">
						<div className="d-flex flex-column gap-3">
							<CommentCard />
							<CommentCard />
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}
