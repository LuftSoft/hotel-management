import { useEffect, useRef, useState } from "react";

export default function PostComment() {
	const starRef = useRef(0);
	const [errorMessage, setErrorMessage] = useState("");
	const handleRateStar = (star) => {
		starRef.current = star;
		const listNode = document.querySelectorAll(".rating-star");
		for (let index = 0; index < listNode.length; index++) {
			const element = listNode[index];
			console.log(element);
			if (index < star) {
				element.classList.add("star-active");
			} else {
				element.classList.remove("star-active");
			}
		}
	};
	const handlePost = () => {
		if (starRef.current === 0) {
			setErrorMessage("Vui lòng chọn số sao!");
		} else {
			setErrorMessage("");
		}
	};
	useEffect(() => {
		// handleRateStar(4);
	}, []);
	return (
		<div className="d-flex flex-column">
			<div className="d-flex gap-4 p-3 border-top border-bottom border-2">
				<div
					className="d-flex flex-column"
					style={{
						flexBasis: "25%",
					}}>
					<div className="d-flex flex-column">
						<div className="d-flex align-items-center">
							<div
								className="me-2 p-1 border border-3 rounded-circle"
								style={{
									width: 64,
									height: 64,
								}}>
								<img className="w-100 h-100" src="/img/user-avatar.png" alt="avatar" />
							</div>
							<div className="d-flex flex-column flex-grow-1 fw-bold">David L</div>
						</div>
					</div>
				</div>
				<div className="d-flex flex-column gap-2 flex-grow-1">
					<div className="d-flex justify-content-between" style={{ cursor: "pointer" }}>
						<div className="d-flex align-items-center justify-content-between">
							<i
								className="fa-solid fa-star rating-star star-no"
								data-index="1"
								onClick={() => {
									handleRateStar(1);
								}}></i>
							<i
								className="fa-solid fa-star rating-star star-no"
								data-index="2"
								onClick={() => {
									handleRateStar(2);
								}}></i>
							<i
								className="fa-solid fa-star rating-star star-no"
								data-index="3"
								onClick={() => {
									handleRateStar(3);
								}}></i>
							<i
								className="fa-solid fa-star rating-star star-no"
								data-index="4"
								onClick={() => {
									handleRateStar(4);
								}}></i>
							<i
								className="fa-solid fa-star rating-star star-no"
								data-index="5"
								onClick={() => {
									handleRateStar(5);
								}}></i>
						</div>
						{/* <div className="text-secondary">16 May 23</div> */}
					</div>
					<div className="d-flex flex-column mt-2">
						<div>
							<input className="form-control" placeholder="Để lại bình luận của bạn..." />
						</div>
					</div>
					<div className="d-flex align-items-center justify-content-between">
						<button type="button" className="btn btn-primary" onClick={handlePost}>
							Đăng
						</button>
						{errorMessage && <label className="alert alert-danger m-0 py-1">{errorMessage}</label>}
					</div>
				</div>
			</div>
		</div>
	);
}
