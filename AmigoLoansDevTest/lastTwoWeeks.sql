SELECT name 
FROM engineers,
	(SELECT last_two_weeks.engineer_id
	FROM
		(SELECT engineer_id, count(Engineer_Id) AS count
		FROM rota 
		WHERE shift BETWEEN date('now','-14 day') AND date('now') 
		GROUP BY Engineer_Id 
		ORDER BY count ASC) last_two_weeks
	WHERE last_two_weeks.count = 1) as eligible
WHERE Engineers.Id = eligible.Engineer_Id
/* Will get a list of names of all engineers who HAVEN'T completed two shifts in a two week period*/