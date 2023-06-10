import classNames from "classnames/bind";

import styles from "./HotelCard.scss";

const cx = classNames.bind(styles);

export default function HotelCard() {
	return (
		<div className="HotelCard__Container">
			<article className="HotelCard__Wrapper">
				<div className="HotelCard__Logo">
					<img src="/img/hotel.webp" alt="hotel" />
				</div>
				<div className="HotelCard__InfoContainer">
					<div className="HotelCard__Detail">1</div>
					<div className="HotelCard__Deal">2</div>
				</div>
			</article>
		</div>
	);
}
