import { useRef } from "react";

export default function FilterHotel({ handleFilter }) {
	const minPriceRef = useRef();
	const maxPriceRef = useRef();
	const handleClick = () => {
		console.log(minPriceRef.current.value);
		const filter = {};
		if (minPriceRef.current.value) {
			filter.minPrice = minPriceRef.current.value;
		} else {
			filter.minPrice = null;
		}
		if (maxPriceRef.current.value) {
			filter.maxPrice = maxPriceRef.current.value;
		} else {
			filter.maxPrice = null;
		}
		handleFilter(filter);
	};
	return (
		<div>
			<div className="mb-3">
				<label>Sắp xếp theo giá:</label>
				<select
					className="form-select"
					aria-label="Default select example"
					onChange={(e) => {
						handleFilter({ priceSort: e.target.value });
					}}>
					<option defaultChecked value="0">
						Tăng dần
					</option>
					<option value="1">Giảm dần</option>
				</select>
			</div>
			<div className="border"></div>
			<div className="mb-3">
				<div className="my-3">
					<label htmlFor="" className="form-label">
						Giá thấp nhất:
					</label>
					<input ref={minPriceRef} className="form-control" id="" type="number" min={0} />
				</div>
				<div className="my-3">
					<label htmlFor="" className="form-label">
						Giá cao nhất:
					</label>
					<input ref={maxPriceRef} className="form-control" id="" type="number" min={0} />
				</div>
				<button type="button" onClick={handleClick} className="btn btn-outline-primary w-100">
					Chọn giá
				</button>
			</div>
			<div className="border"></div>
			<div className="mt-3">
				<label>Dịch vụ:</label>
				<div className="form-check">
					<input
						className="form-check-input"
						onChange={(e) => {
							handleFilter({
								resttaurant: e.target.checked,
							});
						}}
						type="checkbox"
						defaultValue=""
						id="restaurant"
					/>
					<label className="form-check-label" htmlFor="restaurant">
						Nhà hàng
					</label>
				</div>
				<div className="form-check">
					<input
						className="form-check-input"
						type="checkbox"
						onChange={(e) => {
							handleFilter({ allTimeFrontDesk: e.target.checked });
						}}
						defaultValue=""
						id="24h"
					/>
					<label className="form-check-label" htmlFor="24h">
						Lễ tân 24h
					</label>
				</div>
				<div className="form-check">
					<input
						className="form-check-input"
						type="checkbox"
						onChange={(e) => {
							handleFilter({
								elevator: e.target.checked,
							});
						}}
						defaultValue=""
						id="elevator"
					/>
					<label className="form-check-label" htmlFor="elevator">
						Thang máy
					</label>
				</div>
				<div className="form-check">
					<input
						className="form-check-input"
						type="checkbox"
						onChange={(e) => {
							handleFilter({
								pool: e.target.checked,
							});
						}}
						defaultValue=""
						id="pool"
					/>
					<label className="form-check-label" htmlFor="pool">
						Bể bơi
					</label>
				</div>
				<div className="form-check">
					<input
						className="form-check-input"
						type="checkbox"
						onChange={(e) => {
							handleFilter({
								freeBreakfast: e.target.checked,
							});
						}}
						defaultValue=""
						id="free-breakfast"
					/>
					<label className="form-check-label" htmlFor="free-breakfast">
						Bữa sáng miễn phí
					</label>
				</div>
				<div className="form-check">
					<input
						className="form-check-input"
						type="checkbox"
						onChange={(e) => {
							handleFilter({
								airConditioner: e.target.checked,
							});
						}}
						defaultValue=""
						id="air-conditioner"
					/>
					<label className="form-check-label" htmlFor="air-conditioner">
						Máy điều hòa
					</label>
				</div>
				<div className="form-check">
					<input
						className="form-check-input"
						type="checkbox"
						onChange={(e) => {
							handleFilter({
								carBorow: e.target.checked,
							});
						}}
						defaultValue=""
						id="lendingCar"
					/>
					<label className="form-check-label" htmlFor="lendingCar">
						Thuê xe
					</label>
				</div>
				<div className="form-check">
					<input
						className="form-check-input"
						type="checkbox"
						onChange={(e) => {
							handleFilter({
								wifiFree: e.target.checked,
							});
						}}
						defaultValue=""
						id="wifi-free"
					/>
					<label className="form-check-label" htmlFor="wifi-free">
						Wifi free
					</label>
				</div>
				<div className="form-check">
					<input
						className="form-check-input"
						type="checkbox"
						onChange={(e) => {
							handleFilter({
								parking: e.target.checked,
							});
						}}
						defaultValue=""
						id="parking"
					/>
					<label className="form-check-label" htmlFor="parking">
						Chỗ đậu xe
					</label>
				</div>
				<div className="form-check">
					<input
						className="form-check-input"
						type="checkbox"
						onChange={(e) => {
							handleFilter({
								allowPet: e.target.checked,
							});
						}}
						defaultValue=""
						id="allow-pets"
					/>
					<label className="form-check-label" htmlFor="allow-pets">
						Cho phép dắt thú cưng
					</label>
				</div>
			</div>
		</div>
	);
}
