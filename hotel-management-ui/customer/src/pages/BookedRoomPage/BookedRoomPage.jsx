import BookedRoomCard from "../../components/BookedRoomCard";
import { useQuery } from "@tanstack/react-query";
import { axiosJWT, url } from "../../utils/httpRequest";
import { useDispatch, useSelector } from "react-redux";
import { selectAccessToken, selectRefreshToken, selectUser } from "../../redux/selectors";
import EmptyCard from "../../components/EmptyCard/EmptyCard";
import CardSkeleton from "../../components/CardSkeleton/CardSkeleton";
import { Pagination } from "../../components/Pagination";
import { useMemo, useState } from "react";

let pageSize = 2;

export default function BookedRoomPage() {
	const accessToken = useSelector(selectAccessToken);
	const refreshToken = useSelector(selectRefreshToken);
	const currentUser = useSelector(selectUser);

	const dispatch = useDispatch();

	const [currentPage, setCurrentPage] = useState(1);

	const bookedRoomState = useQuery({
		queryKey: ["bookedRoom", currentUser.id],
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
	const currentTableData = useMemo(() => {
		if (bookedRoomState.isSuccess) {
			const firstPageIndex = (currentPage - 1) * pageSize;
			const lastPageIndex = firstPageIndex + pageSize;
			return bookedRooms.slice(firstPageIndex, lastPageIndex);
		} else {
			return [];
		}
	}, [currentPage, bookedRoomState.isSuccess, bookedRoomState.isFetching]);

	const handlePageChange = (page) => {
		setCurrentPage(page);
	};
	const handleRefetch = () => {
		bookedRoomState.refetch();
	};
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
				) : currentTableData.length > 0 ? (
					currentTableData.map((bookedRoom) => (
						<BookedRoomCard key={bookedRoom.id} bookedRoom={bookedRoom} onAbort={handleRefetch} />
					))
				) : (
					<EmptyCard
						title={"Không tìm thấy đặt chỗ"}
						content={
							"Mọi chỗ bạn đặt sẽ được hiển thị tại đây. Hiện bạn chưa có bất kỳ đặt chỗ nào, hãy đặt trên trang chủ ngay!"
						}
					/>
				)}
			</div>
			{bookedRoomState.isSuccess && currentTableData.length > 0 && (
				<div>
					<Pagination
						currentPage={currentPage}
						// totalPage={totalPage}
						totalCount={bookedRooms.length}
						// totalCount={0}
						pageSize={pageSize}
						onPageChange={handlePageChange}
						className={"justify-content-center mt-3"}
					/>
				</div>
			)}
		</div>
	);
}
