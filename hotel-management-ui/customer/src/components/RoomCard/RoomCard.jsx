export default function RoomCard() {
	return (
		<div className="bg-light rounded border p-3">
			<div className="container p-0">
				<div className="d-flex">
					<div className="bg-white p-0">
						<div className="d-flex flex-column">
							<div>
								<div className="mb-1" style={{ height: 144 }}>
									<img src="/img/hotel-room.webp" alt="room" className="w-100 h-100" style={{ objectFit: "fill" }} />
								</div>
							</div>
							<div className="d-flex justify-content-between">
								<div style={{ width: 90, height: 50 }} className="me-1">
									<img src="/img/hotel-room.webp" alt="room" className="w-100 h-100" style={{ objectFit: "fill" }} />
								</div>
								<div style={{ width: 90, height: 50 }} className="me-1">
									<img src="/img/hotel-room.webp" alt="room" className="w-100 h-100" style={{ objectFit: "fill" }} />
								</div>
								<div style={{ width: 90, height: 50 }}>
									<img src="/img/hotel-room.webp" alt="room" className="w-100 h-100" style={{ objectFit: "fill" }} />
								</div>
							</div>
						</div>
					</div>
					<div className="w-100 bg-white ms-3 rounded">
						<div className="d-flex flex-column px-4 py-3">
							<div className="d-flex flex-column">
								<div className="fw-bold fs-6">Standard Double Room</div>
								<div className="d-flex justify-content-between text-secondary">
									<div>
										<i className="fa-solid fa-bed"></i>
										<span className="ms-2">1 Giường đôi</span>
									</div>
									<div>
										<i className="fa-solid fa-user-group"></i>
										<span className="ms-2">2 người</span>
									</div>
									<div className="text-danger">(3 phòng có sẵn)</div>
								</div>
								<div className="border my-3"></div>
							</div>
							<div className="d-flex">
								<div className="flex-grow-1">
									<div className="d-flex flex-column">
										<div className="row">
											<div className="col">
												<div className="d-flex flex-column">
													<div className="d-flex flex-column">
														<div className="d-flex text-muted">
															<i className="fa-solid fa-store"></i>
															<div>Nhà hàng</div>
														</div>
													</div>
													<div className="d-flex flex-column">
														<div className="d-flex text-info">
															<i className="fa-solid fa-elevator"></i>
															<div>Thang máy</div>
														</div>
													</div>
													<div className="d-flex flex-column">
														<div className="d-flex text-info">
															<i className="fa-solid fa-water-ladder"></i>
															<div>Bể bơi</div>
														</div>
													</div>
													<div className="d-flex flex-column">
														<div className="d-flex text-info">
															<i className="fa fa-utensils"></i>
															<div>Bữa sáng miễn phí</div>
														</div>
													</div>
												</div>
											</div>
											<div className="col">
												<div className="d-flex flex-column">
													<div className="d-flex flex-column">
														<div className="d-flex text-info">
															<i className="fa fa-temperature-arrow-down"></i>
															<div>Máy điều hòa</div>
														</div>
													</div>
													<div className="d-flex flex-column">
														<div className="d-flex text-info">
															<i className="fa-sharp fa-solid fa-bicycle"></i>
															<div>Thuê xe</div>
														</div>
													</div>
													<div className="d-flex flex-column">
														<div className="d-flex text-info">
															<i className="fa-solid fa-wifi"></i>
															<div>Wifi miễn phí</div>
														</div>
													</div>
													<div className="d-flex flex-column">
														<div className="d-flex text-info">
															<i className="fa-solid fa-parking"></i>
															<div>Chỗ đậu xe</div>
														</div>
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
								<div className="d-flex flex-column align-items-end" style={{ width: "25%" }}>
									<div className="d-flex flex-column text-end">
										<div>{"600.000 VNĐ"}</div>
										<div className="text-danger">{"450.000 VNĐ"}</div>
										<div>{"/ room / night(s)"}</div>
										<div className="text-primary">{"Giá cuối cùng"}</div>
									</div>
									<div>
										<button className="btn btn-primary">Đặt ngay</button>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}
