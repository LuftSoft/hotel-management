import starIcon from "../../assets/star.svg";
import halfStar from "../../assets/half-star.svg";

export const Stars = ({ numberOfStar }) => {
	const star = Math.floor(numberOfStar);
	const hasHalfStar = numberOfStar - star > 0;
	return (
		<>
			{Array.from({ length: star }).map((_, index) => (
				<div key={index}>
					<img src={starIcon} style={{ width: 18, height: 18 }} />
				</div>
			))}
			{hasHalfStar && (
				<div>
					<img src={halfStar} style={{ width: 18, height: 18 }} />
				</div>
			)}
		</>
	);
};
