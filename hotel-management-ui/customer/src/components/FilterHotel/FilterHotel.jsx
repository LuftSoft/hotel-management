export default function FilterHotel() {
	return (
		<div>
			<div className="mb-3">
				<label>Sắp xếp theo giá:</label>
				<select className="form-select" aria-label="Default select example">
					<option defaultChecked value="0">
						Tăng dần
					</option>
					<option value="1">Giảm dần</option>
				</select>
			</div>
			<div className="my-3">
				<label htmlFor="" className="form-label">
					Giá thấp nhất:
				</label>
				<input className="form-control" id="" type="number" min={0} />
			</div>
			<div className="my-3">
				<label htmlFor="" className="form-label">
					Giá cao nhất:
				</label>
				<input className="form-control" id="" type="number" min={0} />
			</div>
			<div>
				<label>Dịch vụ:</label>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="restaurant" />
					<label className="form-check-label" htmlFor="restaurant">
						Nhà hàng
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="24h" />
					<label className="form-check-label" htmlFor="24h">
						Lễ tân 24h
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="elevator" />
					<label className="form-check-label" htmlFor="elevator">
						Thang máy
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="pool" />
					<label className="form-check-label" htmlFor="pool">
						Bể bơi
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="free-breakfast" />
					<label className="form-check-label" htmlFor="free-breakfast">
						Bữa sáng miễn phí
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="air-conditioner" />
					<label className="form-check-label" htmlFor="air-conditioner">
						Máy điều hòa
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="lendingCar" />
					<label className="form-check-label" htmlFor="lendingCar">
						Thuê xe
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="wifi-free" />
					<label className="form-check-label" htmlFor="wifi-free">
						Wifi free
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="parking" />
					<label className="form-check-label" htmlFor="parking">
						Chỗ đậu xe
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="allow-pets" />
					<label className="form-check-label" htmlFor="allow-pets">
						Cho phép dắt thú cưng
					</label>
				</div>
			</div>
		</div>
	);
}
