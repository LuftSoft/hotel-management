// import classNames from "classnames/bind";

// import styles from "./HotelCard.scss";

// const cx = classNames.bind(import("./HotelCard.scss"));
// const cx = classNames.bind(styles);

import "./HotelCard.scss";
import star from "../../assets/star.svg";
import halfStar from "../../assets/half-star.svg";

export default function HotelCard({ hotel }) {
	const handleClick = () => {
		window.open("/hotel/" + 8, "_blank");
	};
	return (
		<div className="HotelCard__Container" onClick={handleClick}>
			<article className="HotelCard__Wrapper">
				<div className="HotelCard__Logo">
					<img src="/img/hotel.webp" alt="hotel" />
				</div>
				<div className="HotelCard__InfoContainer">
					<div className="HotelCard__Detail py-3">
						<div className="d-flex flex-column px-3 justify-content-between">
							<div className="d-flex flex-column">
								<div>
									<h3 className="fs-6 m-0">{hotel.name}</h3>
								</div>
								<div className="d-flex my-2">
									<div className="me-2">
										<span className="badge rounded-pill bg-primary">Khách sạn</span>
									</div>
									<div className="d-flex align-items-center">
										<div>
											<img src={star} style={{ width: 18, height: 18 }} />
										</div>
										<div>
											<img src={star} style={{ width: 18, height: 18 }} />
										</div>
										<div>
											<img src={star} style={{ width: 18, height: 18 }} />
										</div>
										<div>
											<img src={halfStar} style={{ width: 18, height: 18 }} />
										</div>
									</div>
								</div>
								<div className="d-flex align-items-center mb-2 text-secondary">
									<i className="fa-solid fa-location-dot"></i>
									<div className="ms-1">Bến nghé, Thành phố Hồ Chí Minh</div>
								</div>
								<div className="d-flex">
									<div className="d-flex align-items-center">
										<i className="fa-solid fa-heart text-info me-2"></i>
										<span className="text-info">{"Ấn tượng -  8.4 "}</span>
										<span className="ms-2">{" (65)"}</span>
									</div>
								</div>
							</div>
							<div className="pt-2">
								<div className="d-flex">
									<div className="d-inline-flex flex-column">
										<span
											className="badge rounded-pill text-dark fst-normal px-3 py-2"
											style={{
												backgroundColor: "rgb(255, 244, 239)",
											}}>
											<div className="d-flex align-items-center text-black-50">
												<i className="fa-solid fa-gift text-danger"></i>
												<span className="pe-2"></span>
												<div>Một số phòng có Extra Benefit!</div>
											</div>
										</span>
									</div>
								</div>
							</div>
						</div>
					</div>
					<div className="HotelCard__Deal border-start d-flex flex-column justify-content-between p-3 pe-2">
						<div className="d-flex flex-column">
							<div className="d-flex align-items-center text-success">
								<i className="fa-solid fa-money-bill-wave"></i>
								<span className="ms-2">Tiết kiệm 43%!</span>
							</div>
						</div>
						<div className="d-flex flex-column">
							<span className="text-info" style={{ fontSize: 12 }}>
								Thanh toán khi nhận phòng
							</span>
							<span className="text-danger">{"1.026.000 VNĐ"}</span>
							<span className="text-info">Giá cuối cùng</span>
						</div>
					</div>
				</div>
			</article>
		</div>
	);
}
