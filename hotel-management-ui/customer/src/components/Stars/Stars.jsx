import starIcon from "../../assets/star.svg";
import halfStar from "../../assets/half-star.svg";

export default function Stars({ numberOfStar }) {
	const star = Math.floor(numberOfStar);
	const hasHalfStar = numberOfStar - star > 0;
	return (
		<>
			{Array.from({ length: star }).map((_, index) => (
				// <div key={index}>
				// 	<img src={starIcon} style={{ width: 18, height: 18 }} />
				// </div>
				<img key={index} className="d-block" src={starIcon} alt="star" style={{ width: 18, height: 18 }} />
			))}
			{hasHalfStar && <img className="d-block" src={halfStar} alt="star" style={{ width: 18, height: 18 }} />}
		</>
	);
}
