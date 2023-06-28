import RoomGalleryIndicator from "../RoomGalleryIndicator/RoomGalleryIndicator";

export default function RoomGallery({ room }) {
	return (
		<>
			<div id={`room-${room.id}-room`} className="carousel slide">
				<div
					className="carousel-inner d-flex flex-column"
					style={{
						width: "100%",
						height: "360px",
					}}>
					{room.hotelImageGalleries.map((value, index) => (
						<div key={index} className={`carousel-item ${index === 0 ? "active" : ""}`}>
							<img
								src={value.link}
								style={{
									width: "100%",
									height: "360px",
									objectFit: "cover",
								}}
								className="d-block"
								alt="..."
							/>
						</div>
					))}
				</div>
				<button
					className="carousel-control-prev"
					type="button"
					data-bs-target={`#room-${room.id}-room`}
					data-bs-slide="prev">
					<span className="carousel-control-prev-icon" aria-hidden="true"></span>
					<span className="visually-hidden">Previous</span>
				</button>
				<button
					className="carousel-control-next"
					type="button"
					data-bs-target={`#room-${room.id}-room`}
					data-bs-slide="next">
					<span className="carousel-control-next-icon" aria-hidden="true"></span>
					<span className="visually-hidden">Next</span>
				</button>
			</div>
			<RoomGalleryIndicator room={room} />
		</>
	);
}
