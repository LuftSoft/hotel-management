import { useQuery } from "@tanstack/react-query";
import { useDispatch, useSelector } from "react-redux";

import { axiosJWT, url } from "../../utils/httpRequest";
import CommentCard from "../CommentCard/CommentCard";
import Stars from "../Stars";
import ServiceRating from "../ServiceRating";
import { selectAccessToken, selectRefreshToken } from "../../redux/selectors";

export default function CommentSection({ hotel }) {
	const accessToken = useSelector(selectAccessToken);
	const refreshToken = useSelector(selectRefreshToken);
	const dispatch = useDispatch();
	const bookedRoomState = useQuery({
		queryKey: ["bookedRoom", accessToken],
		queryFn: async () => {
			const axiosJwt = axiosJWT(accessToken, refreshToken, dispatch);
			try {
				const res = await axiosJwt.get(url.bookedRooms, {
					headers: {
						Authorization: `Bearer ${accessToken}`,
					},
				});
				return res.data;
			} catch (error) {
				return Promise.reject(error);
			}
		},
		staleTime: 3 * 60 * 1000,
	});
	let bookedRooms = [];
	if (bookedRoomState.isSuccess) {
		bookedRooms = bookedRoomState.data.bookingList;
	}

	return (
		<div className="d-flex flex-column bg-white rounded">
			<div className="d-flex flex-column gap-3 px-3 py-4">
				<h2 className="fs-5 fw-bolder">Đánh giá từ khách hàng</h2>
				<div className="d-flex flex-column">
					<h3 className="fs-6">Xếp hạng và điểm đánh giá chung</h3>
					<div className="text-secondary" style={{ fontSize: 14 }}>
						{"Từ "}
						<strong>{hotel.comments.length || 0}</strong>
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
											{hotel.star}
										</div>
									</div>
								</div>
								<div className="d-flex flex-column justify-content-center">
									<div className="fs-4 text-center fw-bold text-info">
										<div className="d-flex align-items-center justify-content-between">
											<Stars numberOfStar={hotel.star} />
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
												style={{ width: "15%" }}
												aria-label="rating"
												aria-valuenow="75"
												aria-valuemin="0"
												aria-valuemax="100"></div>
										</div>
									</div>
									<div className="col-2">13</div>
								</div>
								<div className="row">
									<div className="col-3">Rất tốt</div>
									<div className="col-7 px-4">
										<div className="progress">
											<div
												className="progress-bar bg-info"
												role="progressbar"
												style={{ width: "25%" }}
												aria-label="rating"
												aria-valuenow="75"
												aria-valuemin="0"
												aria-valuemax="100"></div>
										</div>
									</div>
									<div className="col-2">23</div>
								</div>
								<div className="row">
									<div className="col-3">Hài lòng</div>
									<div className="col-7 px-4">
										<div className="progress">
											<div
												className="progress-bar bg-info"
												role="progressbar"
												style={{ width: "55%" }}
												aria-label="rating"
												aria-valuenow="75"
												aria-valuemin="0"
												aria-valuemax="100"></div>
										</div>
									</div>
									<div className="col-2">43</div>
								</div>
								<div className="row">
									<div className="col-3">Trung bình</div>
									<div className="col-7 px-4">
										<div className="progress">
											<div
												className="progress-bar bg-info"
												role="progressbar"
												style={{ width: "15%" }}
												aria-label="rating"
												aria-valuenow="75"
												aria-valuemin="0"
												aria-valuemax="100"></div>
										</div>
									</div>
									<div className="col-2">13</div>
								</div>
								<div className="row">
									<div className="col-3">Kém</div>
									<div className="col-7 px-4">
										<div className="progress">
											<div
												className="progress-bar bg-info"
												role="progressbar"
												style={{ width: "5%" }}
												aria-label="rating"
												aria-valuenow="75"
												aria-valuemin="0"
												aria-valuemax="100"></div>
										</div>
									</div>
									<div className="col-2">6</div>
								</div>
							</div>
						</div>
						<div className="d-flex flex-column flex-grow-1 align-items-starts ps-5 pe-4">
							<div>
								{/* 5 */}
								<ServiceRating name="Sạch sẽ" numOfStar={3} />
								<ServiceRating name="Thoải mái" numOfStar={2.5} />
								<ServiceRating name="Đồ ăn" numOfStar={4.5} />
								<ServiceRating name="Vị trí" numOfStar={3.5} />
								<ServiceRating name="Dịch vụ" numOfStar={4} />
							</div>
						</div>
					</div>
				</div>
				{/* list comment */}
				<div className="d-flex flex-column">
					{/* to comment */}
					{/* divider */}
					<div className="my-2"></div>
					{/* list comment */}
					<div className="d-flex flex-column">
						<div className="d-flex flex-column gap-3">
							{hotel.comments.map((comment) => (
								<CommentCard key={comment.id} comment={comment} />
							))}
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}
