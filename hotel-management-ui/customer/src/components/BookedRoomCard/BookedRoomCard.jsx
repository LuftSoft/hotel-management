import { useSelector } from "react-redux";
import { formatDate } from "../../utils/helpers";
import { axiosDelete, axiosGet, url } from "../../utils/httpRequest";
import { selectAccessToken } from "../../redux/selectors";
import { useState } from "react";
import PostComment from "../PostComment";
import { useQuery } from "@tanstack/react-query";

export default function BookedRoomCard({ bookedRoom }) {
	console.log("bookedRoom", bookedRoom);
	const [showPostComment, setShowPostComment] = useState(false);
	const [aborted, setAborted] = useState(false);

	const accessToken = useSelector(selectAccessToken);

	const hotelState = useQuery({
		queryKey: ["hotel", bookedRoom.hotelId],
		queryFn: async () => {
			try {
				const res = await axiosGet(url.detailHotel + bookedRoom.hotelId);
				// console.log(res);
				return res;
			} catch (error) {
				return Promise.reject(error);
			}
		},
		staleTime: 3 * 60 * 1000,
	});

	let hotel = {};
	if (hotelState.isSuccess) {
		hotel = hotelState.data.hotelDto;
	}

	/**
	 * return true if user can post comment to this hotel
	 * @returns {true|false}
	 */
	const hasPosted = () => {
		if (accessToken) {
			for (let index = 0; index < hotel.comments.length; index++) {
				const comment = hotel.comments[index];
				if (comment.bookingId === bookedRoom.id) {
					return true;
				}
			}
		}
		return false;
	};
	/**
	 * get comment if any
	 */
	const getComment = () => {
		if (accessToken) {
			for (let index = 0; index < hotel.comments.length; index++) {
				const comment = hotel.comments[index];
				if (comment.bookingId === bookedRoom.id) {
					return comment;
				}
			}
		}
		return null;
	};

	const handleAbort = async (e) => {
		e.stopPropagation();
		// console.log(bookedRoom.id);
		try {
			const res = await axiosDelete(url.cancelBooking + bookedRoom.id, {
				headers: {
					Authorization: `Bearer ${accessToken}`,
				},
			});
			// console.log(res); // res.success
			if (res.success) {
				setAborted(true);
			}
		} catch (error) {
			console.log(error);
		}
	};
	const handleClick = () => {
		window.open("/hotel/" + bookedRoom.hotelId, "_blank");
	};
	const handleRate = (e) => {
		e.stopPropagation();
		setShowPostComment(!showPostComment);
	};
	return (
		<>
			<div onClick={handleClick} className="border rounded bg-white p-3 d-flex flex-column cursor-pointer">
				<div className="d-flex">
					<div className="border rounded-2" style={{ overflow: "hidden" }}>
						<img
							src={bookedRoom.hotelImage}
							alt="avatar"
							className="rounded"
							onError={(e) => {
								e.target.src = "/img/hotel.webp";
							}}
							style={{ width: 150, height: 100, objectFit: "cover" }}
						/>
					</div>
					<div className="d-flex flex-grow-1 ms-3">
						<div className="w-75 flex-grow-1">
							<div className="d-flex flex-column">
								<div className="fs-5 fw-bold">{bookedRoom.hotelName}</div>
								<div>{`${bookedRoom.roomName} - ${bookedRoom.roomSize} khách`}</div>
								<div>Ngày nhận phòng: {formatDate(new Date(bookedRoom.fromDate), "dd-mm-yyyy")}</div>
								<div>Ngày trả phòng: {formatDate(new Date(bookedRoom.toDate), "dd-mm-yyyy")}</div>
							</div>
						</div>
						{/* check đã trả phòng chưa */}
						<div className="d-flex flex-column justify-content-end">
							{bookedRoom.returned || aborted ? null : (
								<button
									type="button"
									onClick={handleRate}
									data-bs-toggle="collapse"
									// data-bs-target="#test"
									aria-expanded="false"
									aria-controls="test"
									className="btn btn-outline-primary mb-2">
									Đánh giá
								</button>
							)}
							{/* <div> */}
							<button
								type="button"
								onClick={handleAbort}
								className={`btn ${bookedRoom.returned || aborted ? "btn-secondary" : "btn-danger"}`}
								disabled={bookedRoom.returned || aborted ? true : false}>
								{bookedRoom.returned || aborted ? "Đã hủy" : "Hủy"}
							</button>
							{/* </div> */}
						</div>
					</div>
				</div>
				{hotelState.isSuccess && hotel.comments.length > 0 && (
					<div
						// id="test"
						onClick={(e) => {
							e.stopPropagation();
						}}
						className={`collapse mt-2 ${showPostComment ? " show " : ""}`}>
						<PostComment
							posted={hasPosted()}
							comment={getComment()}
							bookedRoomId={bookedRoom.id}
							onPostCommentClick={hotelState.refetch}
						/>
					</div>
				)}
			</div>
		</>
	);
}
