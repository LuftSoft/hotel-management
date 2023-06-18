import { useEffect, useState } from "react";
import { useRef } from "react";
import { formatDate } from "../../utils/helpers";
import { useQuery } from "@tanstack/react-query";
import { axiosGet, url } from "../../utils/httpRequest";
import { useNavigate } from "react-router-dom";
import { routes } from "../../routes";

export default function HeroSection() {
	console.log("render");
	const navigate = useNavigate();
	const checkInDateRef = useRef(null);
	const checkOutDateRef = useRef(null);
	const provinceRef = useRef();
	const districtRef = useRef();
	const homeletRef = useRef();
	const guestRef = useRef();
	const roomRef = useRef();
	// const getDistrict = useRef(false);
	const [getDistrict, setGetDistrict] = useState(false);
	const [getHomelet, setGetHomelet] = useState(false);

	const hotelProvince = useQuery({
		queryKey: ["hotelProvince"],
		queryFn: async () => {
			try {
				const res = await axiosGet(url.province);
				return res;
			} catch (error) {
				return Promise.reject(error);
			}
		},
		staleTime: 3 * 60 * 1000,
		// refetchOnWindowFocus: false,
	});
	const hotelDistrict = useQuery({
		queryKey: ["hotelDistrict"],
		queryFn: async () => {
			try {
				const res = await axiosGet(url.district + provinceRef.current.value);
				return res;
			} catch (error) {
				return Promise.reject(error);
			}
		},
		enabled: getDistrict,
		staleTime: 3 * 60 * 1000,
		// refetchOnWindowFocus: false,
	});
	const hotelHomelet = useQuery({
		queryKey: ["hotelHomelet"],
		queryFn: async () => {
			try {
				const res = await axiosGet(url.homelet + districtRef.current.value);
				return res;
			} catch (error) {
				return Promise.reject(error);
			}
		},
		enabled: getHomelet,
		staleTime: 3 * 60 * 1000,
		// refetchOnWindowFocus: false,
	});

	let provinces = [];
	if (hotelProvince.isSuccess) {
		provinces = hotelProvince.data.data;
	}
	let districts = [];
	if (hotelDistrict.isSuccess) {
		districts = hotelDistrict.data.data;
	}
	let homelets = [];
	if (hotelHomelet.isSuccess) {
		homelets = hotelHomelet.data.data;
	}

	const handleSearch = () => {
		let params = "?";
		if (checkInDateRef.current.value) {
			console.log(checkInDateRef.current.value);
		}
		params += "pageIndex=0&";
		params += "pageSize=10&";
		if (provinceRef.current.value !== "undefined") {
			params += "ProvineId=" + provinceRef.current.value + "&";
		}
		if (districtRef.current.value !== "undefined") {
			params += "DistrictId=" + districtRef.current.value + "&";
		}
		if (homeletRef.current.value !== "undefined") {
			params += "HomeletId=" + homeletRef.current.value + "&";
		}
		if (checkInDateRef.current.value) {
			params += "FromDate=" + checkInDateRef.current.value + "&";
		}
		if (checkOutDateRef.current.value) {
			params += "ToDate=" + checkOutDateRef.current.value + "&";
		}
		params += "RoonCount=" + roomRef.current.value + "&";
		params += "RoomSize=" + guestRef.current.value;
		navigate(routes.hotel + params);
	};
	const handleProvinceChange = (e) => {
		const value = e.target.value;
		if (value !== "undefined") {
			console.log("test");
			if (!getDistrict) {
				setGetDistrict(true);
			}
			districtRef.current.disabled = false;
			hotelDistrict.refetch();
		} else {
			console.log("vo");
			if (getDistrict) {
				setGetDistrict(false);
			}
			districtRef.current.disabled = true;
			districtRef.current.value = undefined;
			homeletRef.current.disabled = true;
			homeletRef.current.value = undefined;
		}
	};
	const handleDistrictChange = (e) => {
		const value = e.target.value;
		if (value !== "undefined") {
			if (!getDistrict) {
				setGetHomelet(true);
			}
			homeletRef.current.disabled = false;
			hotelHomelet.refetch();
		} else {
			if (getDistrict) {
				setGetHomelet(false);
			}
			homeletRef.current.disabled = true;
			homeletRef.current.value = undefined;
		}
	};

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
											<input ref={guestRef} type="number" className="form-control" defaultValue={1} min={0} />
										</div>
										<div className="mb-3">
											<label>Số phòng:</label>
											<input ref={roomRef} type="number" className="form-control" defaultValue={1} min={0} />
										</div>
									</div>
									<div className="col">
										<div className="mb-3">
											<label>Tỉnh/thành phố:</label>
											<select
												ref={provinceRef}
												onChange={handleProvinceChange}
												className="form-select"
												aria-label="Default select example">
												<option defaultChecked value="undefined">
													Chọn tỉnh/thành phố
												</option>
												{provinces?.map((province) => (
													<option key={province.id} value={province.id}>
														{province.name}
													</option>
												))}
											</select>
										</div>
										<div className="mb-3">
											<label>Quận/huyện:</label>
											<select
												ref={districtRef}
												onChange={handleDistrictChange}
												className="form-select"
												disabled
												aria-label="Default select example">
												<option defaultChecked value="undefined">
													Chọn quận/huyện
												</option>
												{districts?.map((district) => (
													<option key={district.id} value={district.id}>
														{district.name}
													</option>
												))}
											</select>
										</div>
										<div className="mb-3">
											<label>Xã/phường:</label>
											<select ref={homeletRef} className="form-select" disabled aria-label="Default select example">
												<option value="undefined" defaultChecked>
													Chọn xã/phường
												</option>
												{homelets?.map((homelet) => (
													<option key={homelet.id} value={homelet.id}>
														{homelet.name}
													</option>
												))}
											</select>
										</div>
										<label className="p-0 m-0" style={{ visibility: "hidden" }}>
											none
										</label>
										<div className="d-grid">
											<button type="button" onClick={handleSearch} className="btn btn-primary">
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
