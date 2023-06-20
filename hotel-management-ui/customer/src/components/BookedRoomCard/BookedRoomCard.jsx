import { useSelector } from "react-redux";
import { formatDate } from "../../utils/helpers";
import { axiosDelete, url } from "../../utils/httpRequest";
import { selectAccessToken } from "../../redux/selectors";
import { useState } from "react";

export default function BookedRoomCard({ bookedRoom }) {
	const accessToken = useSelector(selectAccessToken);
	const [aborted, setAborted] = useState(false);
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
	return (
		<div onClick={handleClick} className="border rounded bg-white p-3 d-flex cursor-pointer">
			<div className="border rounded-2" style={{ overflow: "hidden" }}>
				<img
					src={bookedRoom.hotelImage}
					alt="avatar"
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
					<div>
						<button
							type="button"
							onClick={handleAbort}
							className={`btn ${bookedRoom.returned || aborted ? "btn-secondary" : "btn-danger"}`}
							disabled={bookedRoom.returned || aborted ? true : false}>
							{bookedRoom.returned || aborted ? "Đã hủy" : "Hủy"}
						</button>
					</div>
				</div>
			</div>
		</div>
	);
}
