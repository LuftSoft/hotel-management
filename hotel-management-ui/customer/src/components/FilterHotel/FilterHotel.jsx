export default function FilterHotel() {
	return (
		<div>
			<div className="mb-3">
				<label>Sắp xếp theo giá:</label>
				<select className="form-select" aria-label="Default select example">
					<option defaultChecked value="1">
						Tăng dần
					</option>
					<option value="2">Giảm dần</option>
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
					<input className="form-check-input" type="checkbox" defaultValue="" id="" />
					<label className="form-check-label" htmlFor="">
						Nhà hàng
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="" />
					<label className="form-check-label" htmlFor="">
						All time front desk
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="" />
					<label className="form-check-label" htmlFor="">
						Thang máy
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="" />
					<label className="form-check-label" htmlFor="">
						Bể bơi
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="" />
					<label className="form-check-label" htmlFor="">
						Bữa sáng miễn phí
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="" />
					<label className="form-check-label" htmlFor="">
						Máy điều hòa
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="" />
					<label className="form-check-label" htmlFor="">
						Thuê xe
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="" />
					<label className="form-check-label" htmlFor="">
						Wifi free
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="" />
					<label className="form-check-label" htmlFor="">
						Chỗ đậu xe
					</label>
				</div>
				<div className="form-check">
					<input className="form-check-input" type="checkbox" defaultValue="" id="" />
					<label className="form-check-label" htmlFor="">
						Cho phép dắt thú cưng
					</label>
				</div>
			</div>
		</div>
	);
}
