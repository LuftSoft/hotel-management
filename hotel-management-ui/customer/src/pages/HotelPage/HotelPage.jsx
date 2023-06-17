// import classNames from "classnames/bind";

import "./HotelPage.scss";
import HotelCard from "../../components/HotelCard";
import FilterHotel from "../../components/FilterHotel";
import { Pagination } from "../../components/Pagination";
import { Navigate, redirect, useLocation, useNavigate, useSearchParams } from "react-router-dom";
import { routes } from "../../routes";
import { useEffect } from "react";
import { useQuery } from "@tanstack/react-query";
import { axiosPost, url } from "../../utils/httpRequest";

// const cx = classNames.bind(import("./HotelPage.scss"));

// console.log(cx);

export default function HotelPage() {
	const location = useLocation();
	const searchParams = new URLSearchParams(location.search);
	const params = {};
	searchParams.forEach((value, key) => {
		params[key] = value;
	});
	console.log(params);
	const hotel = useQuery({
		queryKey: ["hotel"],
		queryFn: async () => {
			try {
				const res = await axiosPost(
					url.hotel,
					{},
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
	console.log(hotel);
	return (
		<div className="HotelPage__Container my-3">
			{/* <Navigate to={"/"} replace={true} /> */}
			<div className="HotelPage__Body">
				<div className="HotelPage__Filter p-3 bg-white border rounded">
					<FilterHotel />
				</div>
				<div className="HotelPage__List">
					<div>
						<div className="row gy-3">
							<div className="col">
								<HotelCard />
							</div>
							<div className="col">
								<HotelCard />
							</div>
							<div className="col">
								<HotelCard />
							</div>
						</div>
					</div>
					{/* pagination */}
					<Pagination currentPage={1} totalPage={2} className={"justify-content-center mt-3"} />
				</div>
			</div>
		</div>
	);
}
