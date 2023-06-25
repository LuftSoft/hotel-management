import BookedRoomCard from "../../components/BookedRoomCard";
import { useQuery } from "@tanstack/react-query";
import { axiosJWT, url } from "../../utils/httpRequest";
import { useDispatch, useSelector } from "react-redux";
import { selectAccessToken, selectRefreshToken } from "../../redux/selectors";
import SidebarUser from "../../components/SidebarUser/SidebarUser";
import EmptyCard from "../../components/EmptyCard/EmptyCard";
import CardSkeleton from "../../components/CardSkeleton/CardSkeleton";

export default function BookedRoomPage() {
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
		<div>
			<h1 className="fs-4">Danh sách phòng đã đặt</h1>
			{/* card wrapper, list */}
			<div className="d-flex flex-column gap-3">
				{bookedRoomState.isFetching ? (
					<>
						<CardSkeleton />
						<CardSkeleton />
					</>
				) : bookedRooms.length > 0 ? (
					bookedRooms.map((bookedRoom) => <BookedRoomCard key={bookedRoom.id} bookedRoom={bookedRoom} />)
				) : (
					<EmptyCard
						title={"Không tìm thấy đặt chỗ"}
						content={
							"Mọi chỗ bạn đặt sẽ được hiển thị tại đây. Hiện bạn chưa có bất kỳ đặt chỗ nào, hãy đặt trên trang chủ ngay!"
						}
					/>
				)}
			</div>
		</div>
	);
	// return (
	// 	<div className="bg-light">
	// 		<div className="Container">
	// 			<div className="d-flex flex-column">
	// 				<div className="d-flex gap-4 my-3">
	// 					<SidebarUser />
	// 					<div className="flex-grow-1 d-flex flex-column gap-3">
	// 						{/* cutting here */}

	// 					</div>
	// 				</div>
	// 			</div>
	// 		</div>
	// 	</div>
	// );
}
