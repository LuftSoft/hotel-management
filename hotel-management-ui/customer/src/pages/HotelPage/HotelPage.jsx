import { routes } from "../../routes";
import { useState } from "react";
import { Navigate, useLocation } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
// import classNames from "classnames/bind";

import "./HotelPage.scss";
import HotelCard from "../../components/HotelCard";
import FilterHotel from "../../components/FilterHotel";
import { Pagination } from "../../components/Pagination";
import { axiosPost, url } from "../../utils/httpRequest";
import CardSkeleton from "../../components/CardSkeleton";
import { useDeferred } from "../../hooks";
import EmptyCard from "../../components/EmptyCard/EmptyCard";

// const cx = classNames.bind(import("./HotelPage.scss"));

// console.log(cx);

const initFilters = {
	priceSort: 0,
	minPrice: null,
	maxPrice: null,
	resttaurant: null,
	allTimeFrontDesk: null,
	elevator: null,
	pool: null,
	freeBreakfast: null,
	airConditioner: null,
	carBorow: null,
	wifiFree: null,
	parking: null,
	allowPet: null,
};

export default function HotelPage() {
	const [currentPage, setCurrentPage] = useState(1);
	const [filters, setFilters] = useState(initFilters);
	const filtersDeferred = useDeferred(filters, 500);
	const location = useLocation();
	const searchParams = new URLSearchParams(location.search);
	const params = {};
	searchParams.forEach((value, key) => {
		if (value) {
			switch (key) {
				case "pageIndex":
					if (!isNaN(value)) {
						params[key] = value;
					}
					break;
				case "pageSize":
					if (!isNaN(value)) {
						params[key] = value;
					}
					break;
				case "ProvineId":
					if (!isNaN(value)) {
						params[key] = value;
					}
					break;
				case "DistrictId":
					if (!isNaN(value)) {
						params[key] = value;
					}
					break;
				case "HomeletId":
					if (!isNaN(value)) {
						params[key] = value;
					}
					break;
				case "RoonCount":
					if (!isNaN(value)) {
						params[key] = value;
					}
					break;
				case "RoomSize":
					if (!isNaN(value)) {
						params[key] = value;
					}
					break;
				case "FromDate":
					if (/^\d{4}-\d{2}-\d{2}$/.test(value)) {
						if (!isNaN(Date.parse(value))) {
							params[key] = value;
						}
					}
					break;
				case "ToDate":
					if (/^\d{4}-\d{2}-\d{2}$/.test(value)) {
						if (!isNaN(Date.parse(value))) {
							params[key] = value;
						}
					}
					break;
				default:
					break;
			}
		}
	});
	if (Object.keys(params).length <= 0) {
		return <Navigate to={routes.home} />;
	}
	const listHotelState = useQuery({
		queryKey: ["hotel", params, filtersDeferred],
		queryFn: async () => {
			console.log(filters);
			try {
				const res = await axiosPost(
					url.hotel,
					{
						...filters,
					},
					{
						params: {
							...params,
						},
					},
				);
				console.log(res);
				return res;
			} catch (error) {
				console.log(error);
				return Promise.reject(error);
			}
		},
		staleTime: 3 * 60 * 1000,
	});
	let listHotel = [];
	let categories = [];
	let totalPage = 0;
	if (listHotelState.isSuccess && listHotelState.data.success) {
		// if (false) {
		listHotel = listHotelState.data.hotels;
		categories = listHotelState.data.categories;
		totalPage = listHotelState.data.totalPage;
	}
	//  else {
	// 	return <HotelPageSkeleton />;
	// }
	const handlePageChange = (page) => {
		setCurrentPage(page);
	};
	const handleFilter = (filter) => {
		setFilters({
			...filters,
			...filter,
		});
	};
	return (
		<div className="HotelPage__Container my-4">
			{/* <Navigate to={"/"} replace={true} /> */}
			<div className="HotelPage__Body">
				<div className="HotelPage__Filter p-3 bg-white border rounded">
					<FilterHotel handleFilter={handleFilter} />
				</div>
				<div className="HotelPage__List">
					<div>
						<div className="row gy-3 mx-2">
							{listHotelState.isFetching ? (
								<>
									<CardSkeleton />
									<CardSkeleton />
								</>
							) : listHotel.length > 0 ? (
								listHotel?.map((hotel) => {
									let category = [];
									return (
										<div key={hotel.id} className="col px-0">
											<HotelCard hotel={hotel} />
										</div>
									);
								})
							) : (
								<EmptyCard
									title={"Không tìm thấy khách sạn"}
									content={"Các khách sạn khớp với bộ lọc sẽ được hiển thị tại đây!"}
								/>
							)}
						</div>
					</div>
					{/* pagination */}
					{listHotel.length > 0 && (
						<Pagination
							currentPage={currentPage}
							totalPage={totalPage}
							onPageChange={handlePageChange}
							className={"justify-content-center mt-3"}
						/>
					)}
				</div>
			</div>
		</div>
	);
}
