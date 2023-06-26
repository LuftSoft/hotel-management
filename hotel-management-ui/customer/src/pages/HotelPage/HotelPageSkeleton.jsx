import FilterHotel from "../../components/FilterHotel/FilterHotel";
import HotelCard from "../../components/HotelCard/HotelCard";

export default function HotelPageSkeleton() {
	return (
		<div className="HotelPage__Container my-3">
			{/* <Navigate to={"/"} replace={true} /> */}
			<div className="HotelPage__Body">
				<div className="HotelPage__Filter p-3 border rounded skeleton">
					<div className="" style={{ height: 400 }}></div>
				</div>
				<div className="HotelPage__List">
					<div>
						<div className="row gy-3 px-3">
							{/* <HotelCard /> */}
							<div className="skeleton rounded" style={{ height: 180 }}></div>
							<div className="skeleton rounded" style={{ height: 180 }}></div>
						</div>
					</div>
					<div className="skeleton skeleton-text mt-3"></div>
				</div>
			</div>
		</div>
	);
}
