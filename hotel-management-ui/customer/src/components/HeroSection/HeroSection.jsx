import { useEffect } from "react";
import { useRef } from "react";
import { formatDate } from "../../utils/helpers";

export default function HeroSection() {
	const checkInDateRef = useRef(null);
	const checkOutDateRef = useRef(null);
	useEffect(() => {
		const today = new Date();
		checkInDateRef.current.min = formatDate(today);
	}, []);
	return (
		<section className="position-relative pt-4 pb-5">
			<div
				className="container"
				style={{
					position: "relative",
					zIndex: 5,
				}}>
				<div className="row gx-4">
					<div className="col-lg-4">
						<div className="pt-5 text-white">
							<h1>Sona A Luxury Hotel</h1>
							<p>
								Here are the best hotel booking sites, including recommendations for international travel and for
								finding low-priced hotel rooms.
							</p>
							<a href="#" className="text-white">
								DISCOVER NOW
							</a>
						</div>
					</div>
					<div className="col-lg-6 ms-auto">
						<div className="p-5 rounded bg-white">
							<h3>Tìm và đặt phòng khách sạn</h3>
							<form>
								<div className="row">
									<div className="col">
										<div className="mb-3">
											<label>Ngày nhận phòng:</label>
											<input
												ref={checkInDateRef}
												type="date"
												className="form-control"
												onChange={(e) => {
													const checkInDate = e.target.value;
													if (checkInDate) {
														if (new Date(checkInDate).getTime() > new Date(checkOutDateRef.current.value).getTime()) {
															checkOutDateRef.current.value = null;
														}
														checkOutDateRef.current.min = formatDate(new Date(checkInDate));
														checkOutDateRef.current.disabled = false;
													} else {
														checkOutDateRef.current.value = null;
														checkOutDateRef.current.disabled = true;
													}
												}}
											/>
										</div>
										<div className="mb-3">
											<label>Ngày trả phòng:</label>
											<input ref={checkOutDateRef} disabled type="date" className="form-control" />
										</div>
										<div className="mb-3">
											<label>Số người 1 phòng:</label>
											<input type="number" className="form-control" defaultValue={1} min={0} />
										</div>
										<div className="mb-3">
											<label>Số phòng:</label>
											<input type="number" className="form-control" defaultValue={1} min={0} />
										</div>
									</div>
									<div className="col">
										<div className="mb-3">
											<label>Giá mong muốn (VNĐ):</label>
											<input type="number" className="form-control" min={0} />
										</div>
										<div className="mb-3">
											<label>Tỉnh/thành phố:</label>
											<select className="form-select" aria-label="Default select example">
												<option defaultChecked>Chọn tỉnh/thành phố</option>
												<option value="1">One</option>
												<option value="2">Two</option>
												<option value="3">Three</option>
											</select>
										</div>
										<div className="mb-3">
											<label>Quận/huyện:</label>
											<select className="form-select" disabled aria-label="Default select example">
												<option defaultChecked>Select District</option>
												<option value="1">One</option>
												<option value="2">Two</option>
												<option value="3">Three</option>
											</select>
										</div>
										<div className="mb-3">
											<label>Xã/phường:</label>
											<select className="form-select" disabled aria-label="Default select example">
												<option defaultChecked>Select Commune</option>
												<option value="1">One</option>
												<option value="2">Two</option>
												<option value="3">Three</option>
											</select>
										</div>
										<div className="d-grid mt-4">
											<button type="button" className="btn btn-primary">
												Xem phòng có sẵn
											</button>
										</div>
									</div>
								</div>
							</form>
						</div>
					</div>
				</div>
			</div>
			<div
				style={{
					position: "absolute",
					width: "100%",
					height: "100%",
					top: 0,
					left: 0,
				}}>
				<div id="carouselExampleIndicators" className="carousel slide h-100" data-bs-ride="carousel">
					<div className="carousel-indicators">
						<button
							type="button"
							data-bs-target="#carouselExampleIndicators"
							data-bs-slide-to="0"
							className="active"
							aria-current="true"
							aria-label="Slide 1"></button>
						<button
							type="button"
							data-bs-target="#carouselExampleIndicators"
							data-bs-slide-to="1"
							aria-label="Slide 2"></button>
						<button
							type="button"
							data-bs-target="#carouselExampleIndicators"
							data-bs-slide-to="2"
							aria-label="Slide 3"></button>
					</div>
					<div className="carousel-inner h-100">
						<div className="carousel-item h-100 active">
							<img src="img/hero-1.jpg" className="d-block w-100 h-100" alt="..." />
						</div>
						<div className="carousel-item h-100">
							<img src="img/hero-2.jpg" className="d-block w-100 h-100" alt="..." />
						</div>
						<div className="carousel-item h-100">
							<img src="img/hero-3.jpg" className="d-block w-100 h-100" alt="..." />
						</div>
					</div>
					<button
						className="carousel-control-prev d-none"
						type="button"
						data-bs-target="#carouselExampleIndicators"
						data-bs-slide="prev">
						<span className="carousel-control-prev-icon" aria-hidden="true"></span>
						<span className="visually-hidden">Previous</span>
					</button>
					<button
						className="carousel-control-next d-none"
						type="button"
						data-bs-target="#carouselExampleIndicators"
						data-bs-slide="next">
						<span className="carousel-control-next-icon" aria-hidden="true"></span>
						<span className="visually-hidden">Next</span>
					</button>
				</div>
			</div>
		</section>
	);
}
