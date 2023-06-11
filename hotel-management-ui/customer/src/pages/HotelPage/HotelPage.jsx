// import classNames from "classnames/bind";

import "./HotelPage.scss";
import HotelCard from "../../components/HotelCard";
import FilterHotel from "../../components/FilterHotel";
import { Pagination } from "../../components/Pagination";

// const cx = classNames.bind(import("./HotelPage.scss"));

// console.log(cx);

export default function HotelPage() {
	return (
		<div className="HotelPage__Container my-3">
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
