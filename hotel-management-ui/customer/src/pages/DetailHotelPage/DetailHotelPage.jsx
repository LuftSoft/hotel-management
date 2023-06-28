import { useQuery } from "@tanstack/react-query";
import { useParams } from "react-router";
import { useRef } from "react";

import CommentSection from "../../components/CommentSection";
import { axiosGet, url } from "../../utils/httpRequest";
import Stars from "../../components/Stars";
import RoomCard from "../../components/RoomCard/RoomCard";
import DetailHotelSkeleton from "./DetailHotelSkeleton";

export default function DetailHotelPage() {
	// const { state } = useLocation();
	const roomsRef = useRef();
	const { id } = useParams();
	const hotelState = useQuery({
		queryKey: ["hotel", id],
		queryFn: async () => {
			try {
				const res = await axiosGet(url.detailHotel + id);
				console.log(res);
				return res;
			} catch (error) {
				return Promise.reject(error);
			}
		},
		staleTime: 3 * 60 * 1000,
	});
	const handleChooseRoom = () => {
		roomsRef.current.scrollIntoView({
			behavior: "smooth",
			inline: "center",
		});
	};
	const getMinPrice = (rooms) => {
		let minPrice = rooms[0]?.price || 0;
		for (let index = 0; index < rooms.length; index++) {
			const room = rooms[index];
			if (room.price < minPrice) {
				minPrice = room.price;
			}
		}
		return minPrice;
	};
	let hotel = {};
	if (hotelState.isSuccess) {
		hotel = hotelState.data.hotelDto;
	} else {
		return <DetailHotelSkeleton />;
	}
	const handleRoomCardClick = () => {
		hotelState.refetch();
	};
	return (
		<div className=" bg-light py-3">
			<div className="Container my-3 d-flex flex-column gap-3">
				<div className="bg-white rounded border">
					<div className="d-flex flex-column p-3">
						<div className="d-flex flex-column">
							<h4>{hotel.name}</h4>
							<div className="d-flex align-items-center mb-2">
								<div className="d-flex me-2">
									<span className="badge rounded-pill bg-primary">{hotel.hotelCategory.name}</span>
								</div>
								<div className="d-flex align-items-center">
									<Stars numberOfStar={hotel.star} />
								</div>
							</div>
							<div className="d-flex">
								<i className="fa-solid fa-location-dot text-black-50"></i>
								<div className="ms-2">{hotel.address}</div>
							</div>
							<div className="border my-3"></div>
							<div className="d-flex">
								<div className="d-flex flex-grow-1 me-2" style={{ width: 768, height: 488 }}>
									<img
										src={hotel.logoLink}
										alt="room"
										className="w-100 h-100 rounded"
										style={{ objectFit: "cover" }}
										onError={(e) => {
											e.target.src = "/img/hotel-room.webp";
										}}
									/>
								</div>
								<div className="d-flex flex-column" style={{ width: 152 }}>
									<img
										src={hotel.logoLink}
										alt="room"
										className="w-100 h-100 rounded mb-2"
										style={{ maxHeight: 118, objectFit: "cover" }}
										onError={(e) => {
											e.target.src = "/img/hotel-room.webp";
										}}
									/>
									<img
										src={hotel.logoLink}
										alt="room"
										className="w-100 h-100 rounded mb-2"
										style={{ maxHeight: 118, objectFit: "cover" }}
										onError={(e) => {
											e.target.src = "/img/hotel-room.webp";
										}}
									/>
									<img
										src={hotel.logoLink}
										alt="room"
										className="w-100 h-100 rounded mb-2"
										style={{ maxHeight: 118, objectFit: "cover" }}
										onError={(e) => {
											e.target.src = "/img/hotel-room.webp";
										}}
									/>
									<img
										src={hotel.logoLink}
										alt="room"
										className="w-100 h-100 rounded"
										style={{ maxHeight: 118, objectFit: "cover" }}
										onError={(e) => {
											e.target.src = "/img/hotel-room.webp";
										}}
									/>
								</div>
							</div>
							{/* button select room */}
							<div className="d-flex justify-content-end mt-2">
								<div className="d-flex flex-column align-items-end">
									<div>{"Giá phòng mỗi đêm từ"}</div>
									<div className="text-danger fs-4 fw-bold">
										{new Intl.NumberFormat().format(getMinPrice(hotel.rooms)) + " VNĐ"}
									</div>
									<button onClick={handleChooseRoom} className="btn btn-primary">
										Chọn phòng
									</button>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div
					className="d-flex flex-column rounded px-3 py-4"
					style={{
						backgroundImage:
							"linear-gradient(92deg, rgb(189, 233, 255) 0%, rgb(214, 241, 255) 50%, rgb(214, 241, 255) 100%)",
					}}>
					<div className="d-flex flex-column">
						<h2 ref={roomsRef} className="fs-4">
							Những phòng còn trống tại {hotel.name}
						</h2>
						<div className="d-flex flex-column gap-4">
							{hotel.rooms.map((room) => (
								<RoomCard
									key={room.id}
									room={room}
									hotelBenefit={hotel.hotelBenefit}
									onCardClick={handleRoomCardClick}
								/>
							))}
						</div>
					</div>
				</div>
				{/* Comment Section */}
				<CommentSection hotel={hotel} />
			</div>
		</div>
	);
}
