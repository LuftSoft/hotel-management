export default function BookedRoomCard({ bookedRoom }) {
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
						<div>Ngày nhận phòng: {new Date(bookedRoom.fromDate).toDateString()}</div>
						<div>Ngày trả phòng: {new Date(bookedRoom.toDate).toDateString()}</div>
					</div>
				</div>
				{/* check đã trả phòng chưa */}
				<div className="d-flex flex-column justify-content-end">
					<div>
						{bookedRoom.isReturned ? (
							<button type="button" className="btn btn-danger">
								Hủy
							</button>
						) : (
							<button type="button" className="btn btn-secondary" disabled>
								Đã hủy
							</button>
						)}
					</div>
				</div>
			</div>
		</div>
	);
}
