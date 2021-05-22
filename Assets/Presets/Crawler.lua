local cmd = _cmd();


function angle (limbO, limbI, speed, desiredAngle)
  limbO.MotorSpeed = speed * (desiredAngle - limbI.Angle);
end

angle(cmd.Limbs.Away, _limbs.Away, 2, 80*math.sin(_time.realtimeSinceStartup * 2))
angle(cmd.Limbs.OuterAway, _limbs.OuterAway, 2, 70*math.sin(_time.realtimeSinceStartup * 2))
angle(cmd.Limbs.Facing, _limbs.Facing, 1, 30*math.sin(_time.realtimeSinceStartup) + 20)
angle(cmd.Limbs.OuterFacing, _limbs.OuterFacing, 1, 10*math.sin(_time.realtimeSinceStartup) + 60)

return cmd;